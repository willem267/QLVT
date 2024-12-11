using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class NhaCungCapSdt
{
    public string Sdt { get; set; } = null!;

    public string MaNcc { get; set; } = null!;

    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;
}
