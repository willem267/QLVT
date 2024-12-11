using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLVT.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
    }
}
