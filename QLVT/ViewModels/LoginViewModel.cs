using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLVT.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Tên đăng nhập không được bỏ trống")]
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Mật khẩu không được bỏ trống")]
        [DisplayName("Mật khẩu")]
        [DataType("Password")]
       
        public string Password { get; set; }
    }
}
