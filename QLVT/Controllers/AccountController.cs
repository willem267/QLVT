using Microsoft.AspNetCore.Mvc;
using QLVT.Models;
using System.Linq;

namespace QLVT.Controllers
{
    public class AccountController : Controller
    {
       
        private QlvtContext _context = new QlvtContext();
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
                    HttpContext.Session.SetString("Username", user.TenDn);
                    HttpContext.Session.SetString("Password", user.Mk);
                    HttpContext.Session.SetString("Quyen", user.MaQ);
                    HttpContext.Session.SetString("Nhanvien", user.MaNv);
                    return RedirectToAction("Index", "Home"); 
                }
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác."); 
            } 
            return View(model); 
        }

        public IActionResult Logout() 
        { 
            HttpContext.Session.Remove("Username"); return RedirectToAction("Login"); 
        }
    }
}
