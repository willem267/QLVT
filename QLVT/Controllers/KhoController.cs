using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLVT.Models;

namespace QLVT.Controllers
{
    [Authorize]
    public class KhoController : Controller
    {
        private readonly QlvtContext _context;

        public KhoController(QlvtContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            // Lọc các bản ghi có Mã Kho không phải là "K00"
            var khoList = await _context.Khos.Where(k => k.MaKho != "K00").ToListAsync();
            return View(khoList);
        }


        public IActionResult formThemKho()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Them(Kho kho)
        {
            // Kiểm tra mã kho chỉ chứa chữ cái tiếng Anh và số
            if (!System.Text.RegularExpressions.Regex.IsMatch(kho.MaKho, @"^[a-zA-Z0-9]+$"))
            {
                ModelState.AddModelError("MaKho", "Mã kho chỉ được chứa chữ cái tiếng Anh và số.");
            }

            // Kiểm tra mã kho đã tồn tại (không phân biệt hoa thường)
            var existingMaKho = _context.Khos
                .FirstOrDefault(k => k.MaKho.ToLower() == kho.MaKho.ToLower());

            if (existingMaKho != null)
            {
                // Nếu mã kho trùng, thêm lỗi vào ModelState
                ModelState.AddModelError("MaKho", "Mã kho đã tồn tại. Vui lòng nhập mã khác.");
            }

            // Loại bỏ khoảng trắng và chuyển tên kho thành chữ thường để kiểm tra trùng
            var noSpacesTenKho = kho.TenKho.Replace(" ", "").ToLower();
            var isDuplicateTenKho = _context.Khos
                .Any(k => k.TenKho.Replace(" ", "").ToLower() == noSpacesTenKho && k.MaKho != kho.MaKho);

            if (isDuplicateTenKho)
            {
                ModelState.AddModelError("TenKho", "Tên kho đã tồn tại. Vui lòng nhập tên khác.");
            }

            // Loại bỏ khoảng trắng và chuyển địa chỉ kho thành chữ thường để kiểm tra trùng
            var noSpacesDiaChi = kho.DiaChi.Replace(" ", "").ToLower();
            var isDuplicateDiaChi = _context.Khos
                .Any(k => k.DiaChi.Replace(" ", "").ToLower() == noSpacesDiaChi && k.MaKho != kho.MaKho);

            if (isDuplicateDiaChi)
            {
                ModelState.AddModelError("DiaChi", "Địa chỉ kho đã tồn tại. Vui lòng nhập địa chỉ khác.");
            }

            // Nếu có lỗi, trả lại view với các lỗi
            if (!ModelState.IsValid)
            {
                return View("formThemKho", kho);  // Trả lại view và giữ dữ liệu đã nhập
            }

            // Nếu không trùng mã, tên và địa chỉ hợp lệ, thêm kho mới vào cơ sở dữ liệu
            _context.Khos.Add(kho);
            _context.SaveChanges();

            // Chuyển hướng về trang danh sách kho
            return RedirectToAction("Index");
        }



        public IActionResult XoaKho(string id)
        {
            var kho = _context.Khos.Find(id);

            if (kho == null)
            {
                // Nếu không tìm thấy kho
                return NotFound();
            }

            // Kiểm tra xem kho có đang được sử dụng trong bảng VatTu không
            var isKhoUsedInVatTu = _context.VatTus.Any(vt => vt.MaKho == id);

            if (isKhoUsedInVatTu)
            {
                // Nếu kho đang được sử dụng trong bảng VatTu, hiển thị thông báo
                TempData["ErrorMessage"] = "Không thể xóa kho vì có vật tư đang sử dụng kho này.";
                return RedirectToAction("Index");
            }

            return View(kho); // Nếu không có vật tư nào sử dụng kho này, cho phép xóa
        }

        [HttpPost, ActionName("XoaKho")]
        public IActionResult XoaKhoPost(string id)
        {
            var kho = _context.Khos.Find(id);

            if (kho == null)
            {
                // Nếu không tìm thấy kho
                return NotFound();
            }

            // Kiểm tra xem kho có đang được sử dụng trong bảng VatTu không
            var isKhoUsedInVatTu = _context.VatTus.Any(vt => vt.MaKho == id);

            if (isKhoUsedInVatTu)
            {
                // Nếu kho đang được sử dụng trong bảng VatTu, hiển thị thông báo và không xóa
                TempData["ErrorMessage"] = "Không thể xóa kho vì có vật tư đang sử dụng kho này.";
                return RedirectToAction("Index");
            }

            // Nếu không có vật tư nào sử dụng kho này, thực hiện xóa kho
            _context.Khos.Remove(kho);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult SuaKho(string id)
        {
            var kho = _context.Khos.Find(id);  // Tìm kho theo mã kho (id)
            if (kho == null) return NotFound();  // Nếu không tìm thấy kho, trả về lỗi 404
            return View(kho);  // Trả về view với thông tin kho
        }

        [HttpPost]
        public IActionResult SuaKho(Kho kho)
        {
            // Loại bỏ tất cả các khoảng trắng trong tên kho và địa chỉ kho
            var noSpacesTenKho = kho.TenKho.Replace(" ", "");
            var noSpacesDiaChi = kho.DiaChi.Replace(" ", "");

            // Kiểm tra xem tên kho có bị trùng không (so với các kho khác, không tính kho hiện tại)
            var isDuplicateName = _context.Khos.Any(x => x.TenKho.Replace(" ", "") == noSpacesTenKho && x.MaKho != kho.MaKho);
            if (isDuplicateName)
            {
                ModelState.AddModelError("TenKho", "Tên kho đã tồn tại, vui lòng nhập tên khác.");
            }

            // Kiểm tra xem địa chỉ kho có bị trùng không (so với các kho khác, không tính kho hiện tại)
            var isDuplicateAddress = _context.Khos.Any(x => x.DiaChi.Replace(" ", "") == noSpacesDiaChi && x.MaKho != kho.MaKho);
            if (isDuplicateAddress)
            {
                ModelState.AddModelError("DiaChi", "Địa chỉ kho đã tồn tại, vui lòng nhập địa chỉ khác.");
            }

            // Nếu ModelState không hợp lệ, trả về form sửa kho với thông báo lỗi
            if (!ModelState.IsValid)
            {
                return View(kho);
            }

            // Cập nhật kho nếu không có lỗi
            kho.TenKho = noSpacesTenKho;  // Cập nhật tên kho đã loại bỏ khoảng trắng
            kho.DiaChi = noSpacesDiaChi;  // Cập nhật địa chỉ kho đã loại bỏ khoảng trắng

            _context.Update(kho);
            _context.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            return RedirectToAction("Index");  // Chuyển hướng về trang danh sách kho
        }



    }


}
