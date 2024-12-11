using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class PhieuXuat
{
    public string MaPx { get; set; } = null!;

    public DateOnly NgayXuat { get; set; }

    public string MucDich { get; set; } = null!;

    public string MaKho { get; set; } = null!;

    public string? MaPb { get; set; }

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    public virtual Kho MaKhoNavigation { get; set; } = null!;

    public virtual PhongBan? MaPbNavigation { get; set; }
}
