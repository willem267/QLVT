using System;
using System.Collections.Generic;

namespace QLVT.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string Ho { get; set; } = null!;

    public string? TenDem { get; set; }

    public string Ten { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly NgaySinh { get; set; }

    public bool GioiTinh { get; set; }

    public string MaCv { get; set; } = null!;

    public string MaPb { get; set; } = null!;

    public virtual ChucVu MaCvNavigation { get; set; } = null!;

    public virtual PhongBan MaPbNavigation { get; set; } = null!;

    public virtual ICollection<NhanVienSdt> NhanVienSdts { get; set; } = new List<NhanVienSdt>();

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
