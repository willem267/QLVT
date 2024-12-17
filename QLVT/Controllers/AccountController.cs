using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using QLVT.Models;
using QLVT.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace QLVT.Controllers
{
    public class AccountController : Controller
    {
        private readonly QlvtContext _context;
        public AccountController(QlvtContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.TaiKhoans.FirstOrDefault(u => u.TenDn == model.Username && u.Mk == model.Password);
                if (user != null)
                {
                    // Đăng nhập thành công, lưu thông tin người dùng vào session
                    HttpContext.Session.SetString("Username", user.TenDn);
                    HttpContext.Session.SetString("Quyen", user.MaQ);
                    HttpContext.Session.SetString("Nhanvien", user.MaNv);

                    // Sử dụng Cookie Authentication
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.TenDn),
                        new Claim(ClaimTypes.Role, user.MaQ) // Role có thể là admin, user, v.v.
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Chuyển hướng đến trang sau khi đăng nhập
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Xóa session và sign-out khỏi hệ thống
            HttpContext.Session.Remove("Username");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // Đảm bảo người dùng phải đăng nhập trước khi truy cập vào các trang
        [Authorize]
        public IActionResult SecurePage()
        {
            return View();
        }
    }
}
