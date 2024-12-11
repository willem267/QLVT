using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class TrangThai
{
    public string MaTt { get; set; } = null!;

    public string TenTrangThai { get; set; } = null!;

    public virtual ICollection<VatTu> VatTus { get; set; } = new List<VatTu>();
}
