using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class LoaiVt
{
    public string MaLoai { get; set; } = null!;

    public string TenLoai { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public virtual ICollection<VatTu> VatTus { get; set; } = new List<VatTu>();
}
