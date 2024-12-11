using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class Kho
{
    public string MaKho { get; set; } = null!;

    public string TenKho { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();

    public virtual ICollection<VatTu> VatTus { get; set; } = new List<VatTu>();
}
