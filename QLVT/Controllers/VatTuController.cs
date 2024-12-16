using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLVT.Models;
using System.Text.RegularExpressions;

namespace QLVT.Controllers
{
    public class VatTuController : Controller
    {
        private readonly QlvtContext _context;

        public VatTuController(QlvtContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.VatTus.ToList());
        }


        public IActionResult FormThemVT()
        {
            // Lấy các dữ liệu từ cơ sở dữ liệu
            var loaiVtList = new SelectList(_context.LoaiVts, "MaLoai", "TenLoai");
            var khoList = new SelectList(_context.Khos, "MaKho", "TenKho");
            var trangThaiList = new SelectList(_context.TrangThais, "MaTt", "TenTrangThai");

           

            // Truyền dữ liệu vào View
            ViewData["MaLoaiOptions"] = loaiVtList;
            ViewData["MaKhoOptions"] = khoList;
            ViewData["MaTrangThaiOptions"] = trangThaiList;

            return View();
        }

        [HttpPost]
        public IActionResult Them(VatTu vatTu)
        {
            // Kiểm tra mã vật tư tồn tại hay không
            var existingMaVt = _context.VatTus.FirstOrDefault(vt => vt.MaVt == vatTu.MaVt);
            if (existingMaVt != null)
            {
                ModelState.AddModelError("MaVt", "Mã vật tư đã tồn tại. Vui lòng nhập mã khác.");
            }

            // Kiểm tra mã vật tư chỉ chứa chữ cái tiếng Anh và số
            if (!System.Text.RegularExpressions.Regex.IsMatch(vatTu.MaVt, @"^[a-zA-Z0-9]+$"))
            {
                ModelState.AddModelError("MaVt", "Mã vật tư chỉ được chứa chữ cái tiếng Anh và số, không chứa dấu hoặc ký tự đặc biệt.");
            }

            // Loại bỏ khoảng trắng và kiểm tra tên vật tư đã tồn tại (không phân biệt chữ hoa chữ thường)
            var noSpacesTenVt = vatTu.TenVt.Replace(" ", "").ToLower();
            var existingTenVt = _context.VatTus.FirstOrDefault(vt => vt.TenVt.Replace(" ", "").ToLower() == noSpacesTenVt);

            if (existingTenVt != null)
            {
                ModelState.AddModelError("TenVt", "Tên vật tư đã tồn tại. Vui lòng nhập tên khác.");
            }

            // Kiểm tra nếu số lượng tồn không âm
            if (vatTu.SoLuongTon < 0)
            {
                ModelState.AddModelError("SoLuongTon", "Số lượng tồn không thể là số âm.");
            }

            // Kiểm tra nếu DonGia không phải là số (trong trường hợp người dùng nhập chữ)
            if (!decimal.TryParse(vatTu.DonGia.ToString(), out decimal donGiaValue))
            {
                ModelState.AddModelError("DonGia", "Đơn giá phải là một số hợp lệ.");
            }

            // Kiểm tra nếu đơn giá không hợp lệ (phải là số dương)
            if (vatTu.DonGia <= 0)
            {
                ModelState.AddModelError("DonGia", "Đơn giá phải là một số dương.");
            }

            // Kiểm tra nếu số lượng tồn lớn hơn 0 và trạng thái là "TT00" và mã kho là "K00"
            if (vatTu.SoLuongTon > 0 && (vatTu.MaTt == "TT00" || vatTu.MaKho == "K00"))
            {
                ModelState.AddModelError("SoLuongTon", "Không thể thêm vật tư vì có số lượng tồn có. Không thể thêm hết ở trạng thái và kho.");
            }

            // Nếu có lỗi, truyền lại danh sách lựa chọn cho các ComboBox
            if (!ModelState.IsValid)
            {
                ViewData["MaLoaiOptions"] = new SelectList(_context.LoaiVts, "MaLoai", "TenLoai", vatTu.MaLoai);
                ViewData["MaKhoOptions"] = new SelectList(_context.Khos, "MaKho", "TenKho", vatTu.MaKho);
                ViewData["MaTrangThaiOptions"] = new SelectList(_context.TrangThais, "MaTt", "TenTrangThai", vatTu.MaTt);

                // Trả lại View với model đã có dữ liệu người dùng nhập
                return View("FormThemVT", vatTu);
            }
            else
            {
                // Thêm vật tư vào cơ sở dữ liệu nếu không có lỗi
                _context.VatTus.Add(vatTu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult XoaVT(string id)
        {
            var vt = _context.VatTus.Find(id);

            if (vt == null)
            {
                // Nếu không tìm thấy vật tư
                return NotFound();
            }

            // Kiểm tra xem có phòng ban nào đang sử dụng vật tư này không
            var isUsedInPhongBan = _context.SuDungs.Any(sd => sd.MaVt == id);

            if (isUsedInPhongBan)
            {
                // Nếu có phòng ban đang sử dụng vật tư này, hiển thị thông báo
                TempData["ErrorMessage"] = "Vật tư vẫn còn phòng ban sử dụng, không thể xóa.";
                return RedirectToAction("Index");
            }

            return View(vt); // Nếu không có phòng ban sử dụng, cho phép xóa vật tư
        }

        // Action POST để xóa vật tư
        [HttpPost, ActionName("XoaVT")]
        public IActionResult XoaVTPost(string id)
        {
            var vt = _context.VatTus.Find(id);

            if (vt == null)
            {
                // Xử lý khi không tìm thấy vật tư
                return NotFound();
            }

            // Kiểm tra lại một lần nữa nếu có phòng ban đang sử dụng vật tư này
            var isUsedInPhongBan = _context.SuDungs.Any(sd => sd.MaVt == id);

            if (isUsedInPhongBan)
            {
                // Nếu có phòng ban đang sử dụng vật tư, hiển thị thông báo và không xóa
                TempData["ErrorMessage"] = "Vật tư vẫn còn phòng ban sử dụng, không thể xóa.";
                return RedirectToAction("Index");
            }

            _context.VatTus.Remove(vt);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SuaVT(string id)
        {
            // Tìm vật tư theo mã
            var vatTu = _context.VatTus.Find(id);
            if (vatTu == null)
            {
                return NotFound();
            }

            // Lấy danh sách cho các DropDownList
            ViewData["MaLoaiOptions"] = new SelectList(_context.LoaiVts, "MaLoai", "TenLoai", vatTu.MaLoai);
            ViewData["MaKhoOptions"] = new SelectList(_context.Khos, "MaKho", "TenKho", vatTu.MaKho);
            ViewData["MaTrangThaiOptions"] = new SelectList(_context.TrangThais, "MaTt", "TenTrangThai", vatTu.MaTt);

            return View(vatTu);
        }

        [HttpPost]
        public IActionResult SuaVTPost(VatTu vatTu)
        {
            // Loại bỏ khoảng trắng và kiểm tra tên vật tư có trùng hay không, ngoại trừ vật tư đang sửa
            var noSpacesTenVt = vatTu.TenVt.Replace(" ", "").ToLower();
            var existingtenvt = _context.VatTus.FirstOrDefault(vt => vt.TenVt.Replace(" ", "").ToLower() == noSpacesTenVt && vt.MaVt != vatTu.MaVt);
            if (existingtenvt != null)
            {
                ModelState.AddModelError("TenVt", "Tên vật tư đã tồn tại. Vui lòng nhập tên khác.");
            }

            // Kiểm tra nếu số lượng tồn lớn hơn 0 và trạng thái là "TT00" và mã kho là "K00"
            if (vatTu.SoLuongTon > 0 && (vatTu.MaTt == "TT00" || vatTu.MaKho == "K00"))
            {
                ModelState.AddModelError("", "Không thể sửa vật tư với số lượng tồn và trạng thái/kho không phù hợp.");
            }

            if (!ModelState.IsValid)
            {
                // Nếu có lỗi, truyền lại danh sách lựa chọn cho các ComboBox
                ViewData["MaLoaiOptions"] = new SelectList(_context.LoaiVts, "MaLoai", "TenLoai", vatTu.MaLoai);
                ViewData["MaKhoOptions"] = new SelectList(_context.Khos, "MaKho", "TenKho", vatTu.MaKho);
                ViewData["MaTrangThaiOptions"] = new SelectList(_context.TrangThais, "MaTt", "TenTrangThai", vatTu.MaTt);

                return View("SuaVT", vatTu);
            }

            var existingVatTu = _context.VatTus.Find(vatTu.MaVt);
            if (existingVatTu == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin vật tư
            existingVatTu.TenVt = vatTu.TenVt;
            existingVatTu.DonViTinh = vatTu.DonViTinh;
            existingVatTu.SoLuongTon = vatTu.SoLuongTon;
            existingVatTu.MoTa = vatTu.MoTa;
            existingVatTu.DonGia = vatTu.DonGia;
            existingVatTu.MaLoai = vatTu.MaLoai;
            existingVatTu.MaTt = vatTu.MaTt;
            existingVatTu.MaKho = vatTu.MaKho;

            _context.VatTus.Update(existingVatTu);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult ChiTietVT(string id)
        {
            // Tìm vật tư theo mã
            var vatTu = _context.VatTus
                .Include(v => v.MaLoaiNavigation) // Tham chiếu đến bảng Loại
                .Include(v => v.MaTtNavigation)  // Tham chiếu đến bảng Trạng thái
                .Include(v => v.MaKhoNavigation) // Tham chiếu đến bảng Kho
                .FirstOrDefault(v => v.MaVt == id);

            if (vatTu == null)
            {
                return NotFound();
            }

            // Chuẩn bị dữ liệu chi tiết
            var chiTietVatTu = new
            {
                MaVt = vatTu.MaVt,
                TenVt = vatTu.TenVt,
                DonViTinh = vatTu.DonViTinh,
                SoLuongTon = vatTu.SoLuongTon,
                MoTa = vatTu.MoTa,
                DonGia = vatTu.DonGia,
                Loai = vatTu.MaLoaiNavigation.TenLoai, // Lấy tên loại
                TrangThai = vatTu.MaTtNavigation.TenTrangThai, // Lấy tên trạng thái
                Kho = vatTu.MaKhoNavigation.TenKho // Lấy tên kho
            };

            return View(chiTietVatTu);
        }
        [HttpGet]
        [Route("VatTu/PBSuDung/{maVt}")]
        public async Task<IActionResult> PBSuDung(string maVt)
        {
            // Lấy danh sách SuDung theo mã vật tư, bao gồm thông tin phòng ban và vật tư
            var phongBanVatTus = await _context.SuDungs
                .Where(sd => sd.MaVt == maVt)  // Lọc theo mã vật tư
                .Include(sd => sd.MaPbNavigation)  // Bao gồm thông tin phòng ban
                .Include(sd => sd.MaVtNavigation)  // Bao gồm thông tin vật tư
                .ToListAsync();

            if (!phongBanVatTus.Any())
            {
                TempData["NoPhongBanMessage"] = "Không có phòng ban nào sử dụng vật tư này."; // Lưu thông báo
                return RedirectToAction("Index");  // Chuyển hướng lại trang Index
            }

            // Trả về View với dữ liệu
            return View(phongBanVatTus);
        }



    }
}
