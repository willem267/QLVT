using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QLVT.Models;

namespace QLVT.Controllers
{
    [Authorize]
    public class TaiKhoanController : Controller
    {
        private readonly QlvtContext _context;

        public TaiKhoanController(QlvtContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var  taiKhoans = _context.TaiKhoans
            .Include(tk => tk.MaQNavigation) // Điều kiện Include bảng QUYEN
            .Include(tk => tk.MaNvNavigation) // Điều kiện Include bảng NHAN_VIEN
            .ToList();
           
            return View(taiKhoans);
        }

        [HttpGet]
        public IActionResult formThemTaiKhoan() 
        {
            // Lấy danh sách Tên Quyền từ bảng QUYEN
            ViewBag.QuyenList = new SelectList(
                _context.Quyens.Select(q => new
                {
                    MaQ = q.MaQ,
                    TenQ = q.TenQ
                }),
                "MaQ", // Giá trị của dropdown
                "TenQ" // Hiển thị Tên Quyền
            );

            // Lấy danh sách Họ Tên Nhân Viên từ bảng NHAN_VIEN
            ViewBag.NhanVienList = new SelectList(
                _context.NhanViens.Select(nv => new
                {
                    MaNV = nv.MaNv,
                    HoTen = nv.Ho + " " + nv.TenDem + " " + nv.Ten // Ghép Họ, Tên Đệm, và Tên
                }),
                "MaNV", // Giá trị của dropdown
                "HoTen" // Hiển thị Họ Tên Nhân Viên
            );
            return View();

        }

        [HttpPost]
        public IActionResult themTk(TaiKhoan tk)
        {
            //Kiểm tra trùng mã tài khoản
            var existingMaTK = _context.TaiKhoans.FirstOrDefault(acc => acc.MaTk == tk.MaTk);
            if (existingMaTK != null)
            {
                
                ModelState.AddModelError("MaTk", "Mã tài khoản đã tồn tại. Vui lòng nhập mã khác.");
                
            }
            //Kiểm tra trùng tên đăng nhập
            var existingTenDn = _context.TaiKhoans.FirstOrDefault(acc => acc.TenDn == tk.TenDn);
            if (existingTenDn != null)
            {
                
                ModelState.AddModelError("TenDn", "Tên đăng nhập khoản đã tồn tại. Vui lòng nhập tên đăng nhập khác khác.");
                
            }
            if (!ModelState.IsValid)
            {
                // Lấy danh sách Tên Quyền từ bảng QUYEN
                ViewBag.QuyenList = new SelectList(
                    _context.Quyens.Select(q => new
                    {
                        MaQ = q.MaQ,
                        TenQ = q.TenQ
                    }),
                    "MaQ", // Giá trị của dropdown
                    "TenQ" // Hiển thị Tên Quyền
                );

                // Lấy danh sách Họ Tên Nhân Viên từ bảng NHAN_VIEN
                ViewBag.NhanVienList = new SelectList(
                    _context.NhanViens.Select(nv => new
                    {
                        MaNV = nv.MaNv,
                        HoTen = nv.Ho + " " + nv.TenDem + " " + nv.Ten // Ghép Họ, Tên Đệm, và Tên
                    }),
                    "MaNV", // Giá trị của dropdown
                    "HoTen" // Hiển thị Họ Tên Nhân Viên
                );
                //Trả về lại form với các options người dùng đã chọn
                return View("formThemTaiKhoan", tk);
            }
            else
            {
                //Nếu không lỗi thì thực hiện thêm
                _context.TaiKhoans.Add(tk);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult SuaTK(string id)
        {
            // Tìm tài khoản theo Mã Tài Khoản
            var taiKhoan = _context.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            var tendn = HttpContext.Session.GetString("Username");
            if (taiKhoan.TenDn == tendn)
            {
                TempData["ErrorMessage"] = "Bạn không thể sửa tài khoản của chính mình khi đang đăng nhập.";
                return RedirectToAction("Index");
            }

            // Lấy danh sách Tên Quyền từ bảng QUYEN
            ViewData["QuyenList"] = new SelectList(
                _context.Quyens, "MaQ", "TenQ", taiKhoan.MaQ
            );

            // Lấy danh sách Họ Tên Nhân Viên từ bảng NHAN_VIEN
            ViewData["NhanVienList"] = new SelectList(
                _context.NhanViens.Select(nv => new
                {
                    MaNV = nv.MaNv,
                    HoTen = nv.Ho + " " + nv.TenDem + " " + nv.Ten
                }),
                "MaNV", "HoTen", taiKhoan.MaNv
            );

            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTK(TaiKhoan taiKhoan)
        {
            // Kiểm tra Tên Đăng Nhập có bị trùng không (ngoại trừ chính tài khoản đang sửa)
           
            var existingTenDn = _context.TaiKhoans
                .FirstOrDefault(tk => tk.TenDn==taiKhoan.TenDn && tk.MaTk != taiKhoan.MaTk);
            if (existingTenDn != null)
            {
                ModelState.AddModelError("TenDn", "Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác.");
            }

            // Kiểm tra ModelState có hợp lệ không
            if (!ModelState.IsValid)
            {
                // Truyền lại dữ liệu cho DropDownList nếu có lỗi
                ViewData["QuyenList"] = new SelectList(
                    _context.Quyens, "MaQ", "TenQ", taiKhoan.MaQ
                );

                ViewData["NhanVienList"] = new SelectList(
                    _context.NhanViens.Select(nv => new
                    {
                        MaNV = nv.MaNv,
                        HoTen = nv.Ho + " " + nv.TenDem + " " + nv.Ten
                    }),
                    "MaNV", "HoTen", taiKhoan.MaNv
                );

                return View(taiKhoan);
            }

            // Tìm tài khoản hiện tại
            var existingTaiKhoan = _context.TaiKhoans.Find(taiKhoan.MaTk);
            if (existingTaiKhoan == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin tài khoản
            existingTaiKhoan.TenDn = taiKhoan.TenDn;
            existingTaiKhoan.Mk = taiKhoan.Mk;
            existingTaiKhoan.MaQ = taiKhoan.MaQ;
            existingTaiKhoan.MaNv = taiKhoan.MaNv;

            _context.TaiKhoans.Update(existingTaiKhoan);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]//Xóa tài khoản
        public IActionResult XoaTK(string id)
        {
            var tk = _context.TaiKhoans.Find(id);

            if (tk == null)
            {
                // Nếu không tìm thấy 
                return NotFound();
            }
            var tendn= HttpContext.Session.GetString("Username");
            if(tk.TenDn == tendn)
            {
                TempData["ErrorMessage"] = "Bạn không thể xóa tài khoản của chính mình.";
                return RedirectToAction("Index");
            }
            //Tìm thấy thì trả về view
            return View(tk); 
        }

        // Action POST để xóa vật tư
        [HttpPost, ActionName("XoaTK")]
        public IActionResult XoaTKPost(string id)
        {
            var tk = _context.TaiKhoans.Find(id);

            if (tk == null)
            {
                // Xử lý khi không tìm thấy vật tư
                return NotFound();
            }
            else
            {
                _context.TaiKhoans.Remove(tk);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           
        }

    }
}
