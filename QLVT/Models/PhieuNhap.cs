using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class PhieuNhap
{
    public string MaPn { get; set; } = null!;

    public DateOnly NgayNhap { get; set; }

    public string MaKho { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public string MaNcc { get; set; } = null!;

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual Kho MaKhoNavigation { get; set; } = null!;

    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
