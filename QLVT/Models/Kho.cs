using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace QLVT.Models;

public partial class Kho
{
    [Required(ErrorMessage = "Mã kho không được bỏ trống")]
    [DisplayName("Mã kho")]
    public string MaKho { get; set; } = null!;


    [Required(ErrorMessage = "Tên kho không được bỏ trống")]
    [DisplayName("Tên kho")]
    public string TenKho { get; set; } = null!;

    [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
    [DisplayName("Địa chỉ")]
    public string DiaChi { get; set; } = null!;

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();

    public virtual ICollection<VatTu> VatTus { get; set; } = new List<VatTu>();
}
