using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class Quyen
{
    public string MaQ { get; set; } = null!;

    public string TenQ { get; set; } = null!;

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
