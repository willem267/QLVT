using Microsoft.AspNetCore.Mvc;
using QLVT.Models;
using QLVT.ViewModels;
using System.Linq;

namespace QLVT.Controllers
{
    public class AccountController : Controller
    {

        private readonly QlvtContext _context;
        public AccountController(QlvtContext context)
        { _context = context; }
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

        [HttpPost]
        public IActionResult Logout() 
        { 
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login"); 
        }
    }//sua gi do thêm nua
}
