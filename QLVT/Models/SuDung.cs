using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class SuDung
{
    public int SoLuong { get; set; }

    public string MaVt { get; set; } = null!;

    public string MaPb { get; set; } = null!;

    public virtual PhongBan MaPbNavigation { get; set; } = null!;

    public virtual VatTu MaVtNavigation { get; set; } = null!;
}
