using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class NhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string TenNcc { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<NhaCungCapSdt> NhaCungCapSdts { get; set; } = new List<NhaCungCapSdt>();

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();
}
