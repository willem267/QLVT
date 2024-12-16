using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace QLVT.Models;

public partial class LoaiVt
{
    [Required(ErrorMessage = "Mã loại không được bỏ trống")]
    [DisplayName("Mã vật tư")]
    public string MaLoai { get; set; } = null!;

    [Required(ErrorMessage = "Tên loại không được bỏ trống")]
    [DisplayName("Tên loại vật tư")]
    public string TenLoai { get; set; } = null!;

    [Required(ErrorMessage = "Mô tả không được bỏ trống")]
    [DisplayName("Mô tả")]
    public string MoTa { get; set; } = null!;

    public virtual ICollection<VatTu> VatTus { get; set; } = new List<VatTu>();
}
