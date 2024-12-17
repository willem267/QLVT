using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLVT.Models;

public partial class TaiKhoan
{
    [Required(ErrorMessage ="Không được bỏ trống")]
    [DisplayName("Mã tài khoản")]
    public string MaTk { get; set; } = null!;
    [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
    [DisplayName("Tên đăng nhập")]
    public string TenDn { get; set; } = null!;
    [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
    [DisplayName("Mật khẩu")]
    public string Mk { get; set; } = null!;
    [Required(ErrorMessage = "Phải chọn quyền")]
    [DisplayName("Mã quyền")]
    public string MaQ { get; set; } = null!;
    [Required(ErrorMessage = "Tài khoản phải thuộc về một người sử dụng")]
    [DisplayName("Mã nhân viên")]
    public string MaNv { get; set; } = null!;
    [ValidateNever]
    public virtual NhanVien MaNvNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual Quyen MaQNavigation { get; set; } = null!;
}
