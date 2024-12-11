using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class VatTu
{
    public string MaVt { get; set; } = null!;

    public string TenVt { get; set; } = null!;

    public string DonViTinh { get; set; } = null!;

    public int SoLuongTon { get; set; }

    public string MoTa { get; set; } = null!;

    public decimal DonGia { get; set; }

    public string MaLoai { get; set; } = null!;

    public string MaTt { get; set; } = null!;

    public string MaKho { get; set; } = null!;

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    public virtual Kho MaKhoNavigation { get; set; } = null!;

    public virtual LoaiVt MaLoaiNavigation { get; set; } = null!;

    public virtual TrangThai MaTtNavigation { get; set; } = null!;

    public virtual ICollection<SuDung> SuDungs { get; set; } = new List<SuDung>();
}
