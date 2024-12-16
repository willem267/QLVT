using Microsoft.AspNetCore.Mvc;
using QLVT.Models;
using System.Text.RegularExpressions;

namespace QLVT.Controllers
{

    public class LoaiVtController : Controller
    {
        private readonly QlvtContext _context;
        public LoaiVtController(QlvtContext context)
        { _context = context; }

        public IActionResult Index()
        {
            return View(_context.LoaiVts.ToList());
        }

        public IActionResult formThemLoaiVT()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Them(LoaiVt lvt)
        {
            // Kiểm tra mã chỉ chứa chữ cái tiếng Anh và số
            if (!System.Text.RegularExpressions.Regex.IsMatch(lvt.MaLoai, @"^[a-zA-Z0-9]+$"))
            {
                ModelState.AddModelError("MaLoai", "Mã chỉ được chứa chữ cái tiếng Anh và số.");
            }

            // Kiểm tra mã loại đã tồn tại (không phân biệt hoa thường)
            var existingMaLoai = _context.LoaiVts
                .FirstOrDefault(vt => vt.MaLoai.ToLower() == lvt.MaLoai.ToLower());

            if (existingMaLoai != null)
            {
                // Nếu mã loại trùng, thêm lỗi vào ModelState
                ModelState.AddModelError("MaLoai", "Mã loại đã tồn tại. Vui lòng nhập mã khác.");
            }

            // Kiểm tra nếu tên loại đã tồn tại (không phân biệt hoa thường)
            var existingTenLoai = _context.LoaiVts
                .FirstOrDefault(vt => vt.TenLoai.ToLower() == lvt.TenLoai.ToLower());

            if (existingTenLoai != null)
            {
                // Nếu tên loại trùng, thêm lỗi vào ModelState
                ModelState.AddModelError("TenLoai", "Tên loại đã tồn tại. Vui lòng nhập tên khác.");
            }

            // Nếu có lỗi, trả lại view với các lỗi
            if (!ModelState.IsValid)
            {
                return View("formThemLoaiVT", lvt);  // Trả lại view và giữ dữ liệu đã nhập
            }

            // Nếu không trùng mã và hợp lệ, thêm loại vật tư mới vào cơ sở dữ liệu
            _context.LoaiVts.Add(lvt);
            _context.SaveChanges();

            // Chuyển hướng về trang danh sách
            return RedirectToAction("Index");
        }

        public IActionResult XoaLoaiVT(string id)
        {
            var lvt = _context.LoaiVts.Find(id);

            if (lvt == null)
            {
                // Nếu không tìm thấy loại vật tư
                return NotFound();
            }

            // Kiểm tra xem loại vật tư có đang được sử dụng trong bảng VatTu không
            var isLoaiVtUsedInVatTu = _context.VatTus.Any(vt => vt.MaLoai == id);

            if (isLoaiVtUsedInVatTu)
            {
                // Nếu có vật tư đang sử dụng loại này, hiển thị thông báo
                TempData["ErrorMessage"] = "Không thể xóa loại vật tư vì có vật tư đang sử dụng loại này.";
                return RedirectToAction("Index");
            }

            return View(lvt); // Nếu không có vật tư nào sử dụng loại này, cho phép xóa
        }


        [HttpPost, ActionName("XoaLoaiVT")]
        public IActionResult XoaLoaiVTPost(string id)
        {
            var lvt = _context.LoaiVts.Find(id);

            if (lvt == null)
            {
                // Xử lý khi không tìm thấy loại vật tư
                return NotFound();
            }

            // Kiểm tra xem loại vật tư có đang được sử dụng trong bảng VatTu không
            var isLoaiVtUsedInVatTu = _context.VatTus.Any(vt => vt.MaLoai == id);

            if (isLoaiVtUsedInVatTu)
            {
                // Nếu có vật tư đang sử dụng loại này, hiển thị thông báo và không xóa
                TempData["ErrorMessage"] = "Không thể xóa loại vật tư vì có vật tư đang sử dụng loại này.";
                return RedirectToAction("Index");
            }

            // Nếu không có vật tư nào sử dụng loại này, thực hiện xóa loại vật tư
            _context.LoaiVts.Remove(lvt);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }




        public IActionResult SuaLoaiVT(string id)
        {
            var account = _context.LoaiVts.Find(id);
            if (account == null) return NotFound();
            return View(account);
        }

        [HttpPost]
        public IActionResult SuaLoaiVT(LoaiVt lvt)
        {
            var isDuplicate = _context.LoaiVts.Any(x => x.TenLoai == lvt.TenLoai && x.MaLoai != lvt.MaLoai);
            if (isDuplicate)
            {
                ModelState.AddModelError("TenLoai", "Tên loại vật tư đã tồn tại, vui lòng nhập tên khác.");
            }

            // Nếu ModelState không hợp lệ, trả về form sửa
            if (!ModelState.IsValid)
            {
                return View(lvt);
            }

            // Cập nhật dữ liệu nếu không trùng
            _context.Update(lvt);
            _context.SaveChanges();
            return RedirectToAction("Index");
        
        }
    }
}