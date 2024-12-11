using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class TaiKhoan
{
    public string MaTk { get; set; } = null!;

    public string TenDn { get; set; } = null!;

    public string Mk { get; set; } = null!;

    public string MaQ { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;

    public virtual Quyen MaQNavigation { get; set; } = null!;
}
