using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLVT.Models;


public partial class VatTu
{
    [Required(ErrorMessage = "Mã vật tư không được bỏ trống")]
    [DisplayName("Mã vật tư")]
    public string MaVt { get; set; } = null!;

    [Required(ErrorMessage = "Tên vật tư không được bỏ trống")]
    [DisplayName("Tên vật tư")]
    public string TenVt { get; set; } = null!;

    [Required(ErrorMessage = "đơn vị tồn không được bỏ trống")]
    [DisplayName("Đơn vị tính")]
    public string DonViTinh { get; set; } = null!;

    [Required(ErrorMessage = "Số lượng tồn không được bỏ trống")]
    [DisplayName("Số Lượng Tồn")]
    [Range(1, 500, ErrorMessage = "số lượng chỉ được nhập không dưới 0 và quá 500")]
    public int SoLuongTon { get; set; }

    [Required(ErrorMessage = "Mô tả không được bỏ trống")]
    [DisplayName("Mô rả")]
    public string MoTa { get; set; } = null!;

    [Required(ErrorMessage = "Đơn giá không được bỏ trống")]
    [DisplayName("Đơn giá")]
    [Range(1000, double.MaxValue, ErrorMessage = "Đơn giá phải là một số dương và lớn hơn 1000.")]
    public decimal DonGia { get; set; }

    [Required(ErrorMessage = "Mã loại không được bỏ trống")]
    [DisplayName("Loại")]
    public string MaLoai { get; set; } = null!;

    [Required(ErrorMessage = "Mã trạng thái không được bỏ trống")]
    [DisplayName("Trạng thái")]
    public string MaTt { get; set; } = null!;

    [Required(ErrorMessage = "Mã kho không được bỏ trống")]
    [DisplayName("Kho")]
    public string MaKho { get; set; } = null!;

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    [ValidateNever]
    public virtual Kho MaKhoNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual LoaiVt MaLoaiNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual TrangThai MaTtNavigation { get; set; } = null!;

  
    public virtual ICollection<SuDung> SuDungs { get; set; } = new List<SuDung>();

    
}
