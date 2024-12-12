using Microsoft.AspNetCore.Mvc;
using QLVT.Models;

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
            var existingMaLoai = _context.LoaiVts.FirstOrDefault(vt => vt.MaLoai == lvt.MaLoai);

            if (existingMaLoai != null)
            {
                // Nếu mã loại trùng, thêm lỗi vào ModelState
                ModelState.AddModelError("MaLoai", "Mã vật tư đã tồn tại. Vui lòng nhập mã khác.");
            }

            // Kiểm tra nếu tên loại đã tồn tại
            var existingTenLoai = _context.LoaiVts.FirstOrDefault(vt => vt.TenLoai == lvt.TenLoai);

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

            // Nếu không trùng mã, thêm loại vật tư mới vào cơ sở dữ liệu
            _context.LoaiVts.Add(lvt);
            _context.SaveChanges();

            // Chuyển hướng về trang danh sách
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult XoaLoaiVT(String id)
        {
            var lvt = _context.LoaiVts.Find(id);
            return View(lvt);
        }


        [HttpPost, ActionName("XoaLoaiVT")]
        public IActionResult XoaLoaiVTPost(String id)
        {
            var lvt = _context.LoaiVts.Find(id);
            if (lvt == null)
            {
                // Xử lý khi không tìm thấy LoaiVT
                return NotFound();
            }

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
            if (ModelState.IsValid)
            {
                _context.LoaiVts.Update(lvt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lvt);
        }
    }
}