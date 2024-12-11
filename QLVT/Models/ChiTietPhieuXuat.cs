using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class ChiTietPhieuXuat
{
    public int SoLuong { get; set; }

    public string MaVt { get; set; } = null!;

    public string MaPx { get; set; } = null!;

    public virtual PhieuXuat MaPxNavigation { get; set; } = null!;

    public virtual VatTu MaVtNavigation { get; set; } = null!;
}
