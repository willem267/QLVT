using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class NhanVienSdt
{
    public string Sdt { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
