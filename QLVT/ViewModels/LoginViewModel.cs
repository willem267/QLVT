using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLVT.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Mật khẩu")]
        [DataType("Password")]
        public string Password { get; set; }
    }
}
