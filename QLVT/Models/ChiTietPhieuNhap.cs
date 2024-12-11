using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class ChiTietPhieuNhap
{
    public int SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public decimal ThanhTien { get; set; }

    public string MaPn { get; set; } = null!;

    public string MaVt { get; set; } = null!;

    public virtual PhieuNhap MaPnNavigation { get; set; } = null!;

    public virtual VatTu MaVtNavigation { get; set; } = null!;
}
