using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLVT.Models;

namespace QLVT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       

        private readonly QlvtContext _context;

        public HomeController(QlvtContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Thống kê số lượng vật tư
            var totalVatTu = _context.VatTus.Count();
            var totalLoaiVatTu = _context.LoaiVts.Count();
            var totalTaiKhoan = _context.TaiKhoans.Count();
            var totalKho = _context.Khos.Count();

            // Tổng số lượng vật tư tồn
            var totalQuantity = _context.VatTus.Sum(vt => vt.SoLuongTon);

            // Đưa dữ liệu vào ViewBag
            ViewBag.TotalVatTu = totalVatTu;
            ViewBag.TotalLoaiVatTu = totalLoaiVatTu;
            ViewBag.TotalTaiKhoan = totalTaiKhoan;
            ViewBag.TotalKho = totalKho;
            ViewBag.TotalQuantity = totalQuantity;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
