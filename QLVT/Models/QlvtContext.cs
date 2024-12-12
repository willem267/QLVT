using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLVT.Models;

public partial class QlvtContext : DbContext
{
    public QlvtContext()
    {
    }

    public QlvtContext(DbContextOptions<QlvtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    public virtual DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<Kho> Khos { get; set; }

    public virtual DbSet<LoaiVt> LoaiVts { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhaCungCapSdt> NhaCungCapSdts { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NhanVienSdt> NhanVienSdts { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<SuDung> SuDungs { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    public virtual DbSet<VatTu> VatTus { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=LAPTOP-VLK11DLH\\SQLEXPRESS;Database=QLVT;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
        {
            entity.HasKey(e => new { e.MaPn, e.MaVt }).HasName("PK__ChiTietP__D557B6F34D6F00F6");

            entity.ToTable("ChiTietPhieuNhap");

            entity.Property(e => e.MaPn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPN");
            entity.Property(e => e.MaVt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaPnNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.MaPn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPhi__MaPN__6EF57B66");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.MaVt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPhi__MaVT__6FE99F9F");
        });

        modelBuilder.Entity<ChiTietPhieuXuat>(entity =>
        {
            entity.HasKey(e => new { e.MaVt, e.MaPx }).HasName("PK__ChiTietP__95574E4296EE8AE3");

            entity.ToTable("ChiTietPhieuXuat");

            entity.Property(e => e.MaVt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.MaPx)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPX");

            entity.HasOne(d => d.MaPxNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.MaPx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPhi__MaPX__73BA3083");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.MaVt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPhi__MaVT__72C60C4A");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaCv).HasName("PK__CHUC_VU__27258E7602D2B5BE");

            entity.ToTable("CHUC_VU");

            entity.Property(e => e.MaCv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaCV");
            entity.Property(e => e.TenCv)
                .HasMaxLength(50)
                .HasColumnName("TenCV");
        });

        modelBuilder.Entity<Kho>(entity =>
        {
            entity.HasKey(e => e.MaKho).HasName("PK__KHO__3BDA93500281E6F1");

            entity.ToTable("KHO");

            entity.Property(e => e.MaKho)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.TenKho).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiVt>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LOAI_VT__730A5759E49E810D");

            entity.ToTable("LOAI_VT");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(100);
            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NHA_CUNG__3A185DEB2AAE466E");

            entity.ToTable("NHA_CUNG_CAP");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenNcc)
                .HasMaxLength(50)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<NhaCungCapSdt>(entity =>
        {
            entity.HasKey(e => new { e.Sdt, e.MaNcc }).HasName("PK__NHA_CUNG__69B8B57A712CA5CE");

            entity.ToTable("NHA_CUNG_CAP_SDT");

            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNCC");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.NhaCungCapSdts)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHA_CUNG___MaNCC__5AEE82B9");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NHAN_VIE__2725D70AB6C690B6");

            entity.ToTable("NHAN_VIEN");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ho).HasMaxLength(10);
            entity.Property(e => e.MaCv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaCV");
            entity.Property(e => e.MaPb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPB");
            entity.Property(e => e.Ten).HasMaxLength(10);
            entity.Property(e => e.TenDem).HasMaxLength(10);

            entity.HasOne(d => d.MaCvNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaCv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHAN_VIEN__MaCV__628FA481");

            entity.HasOne(d => d.MaPbNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaPb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHAN_VIEN__MaPB__6383C8BA");
        });

        modelBuilder.Entity<NhanVienSdt>(entity =>
        {
            entity.HasKey(e => new { e.Sdt, e.MaNv }).HasName("PK__NHAN_VIE__786B6DD4BF479D39");

            entity.ToTable("NHAN_VIEN_SDT");

            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNV");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.NhanVienSdts)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHAN_VIEN___MaNV__76969D2E");
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasKey(e => e.MaPn).HasName("PK__PHIEU_NH__2725E7F0C2DF8C06");

            entity.ToTable("PHIEU_NHAP");

            entity.Property(e => e.MaPn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPN");
            entity.Property(e => e.MaKho)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNV");

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.MaKho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHIEU_NHA__MaKho__6A30C649");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHIEU_NHA__MaNCC__6C190EBB");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHIEU_NHAP__MaNV__6B24EA82");
        });

        modelBuilder.Entity<PhieuXuat>(entity =>
        {
            entity.HasKey(e => e.MaPx).HasName("PK__PHIEU_XU__2725E7CA338B1457");

            entity.ToTable("PHIEU_XUAT");

            entity.Property(e => e.MaPx)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPX");
            entity.Property(e => e.MaKho)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaPb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPB");
            entity.Property(e => e.MucDich).HasMaxLength(100);

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.MaKho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHIEU_XUA__MaKho__571DF1D5");

            entity.HasOne(d => d.MaPbNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.MaPb)
                .HasConstraintName("FK__PHIEU_XUAT__MaPB__5812160E");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.MaPb).HasName("PK__PHONG_BA__2725E7E41D529599");

            entity.ToTable("PHONG_BAN");

            entity.Property(e => e.MaPb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPB");
            entity.Property(e => e.TenPb)
                .HasMaxLength(50)
                .HasColumnName("TenPB");
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.MaQ).HasName("PK__QUYEN__C7977BA7BA4A3B2A");

            entity.ToTable("QUYEN");

            entity.Property(e => e.MaQ)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenQ)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SuDung>(entity =>
        {
            entity.HasKey(e => new { e.MaVt, e.MaPb }).HasName("PK__SuDung__75574E40BD013EA5");

            entity.ToTable("SuDung");

            entity.Property(e => e.MaVt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.MaPb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaPB");

            entity.HasOne(d => d.MaPbNavigation).WithMany(p => p.SuDungs)
                .HasForeignKey(d => d.MaPb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SuDung__MaPB__7A672E12");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.SuDungs)
                .HasForeignKey(d => d.MaVt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SuDung__MaVT__797309D9");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TAI_KHOA__27250070181C152B");

            entity.ToTable("TAI_KHOAN");

            entity.Property(e => e.MaTk)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaTK");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.MaQ)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Mk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MK");
            entity.Property(e => e.TenDn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TenDN");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TAI_KHOAN__MaNV__6754599E");

            entity.HasOne(d => d.MaQNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaQ)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TAI_KHOAN__MaQ__66603565");
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTt).HasName("PK__TRANG_TH__2725007955B03ACA");

            entity.ToTable("TRANG_THAI");

            entity.Property(e => e.MaTt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaTT");
            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<VatTu>(entity =>
        {
            entity.HasKey(e => e.MaVt).HasName("PK__VAT_TU__2725103EFF5108C7");

            entity.ToTable("VAT_TU");

            entity.Property(e => e.MaVt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DonViTinh).HasMaxLength(10);
            entity.Property(e => e.MaKho)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaTt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaTT");
            entity.Property(e => e.MoTa).HasMaxLength(100);
            entity.Property(e => e.TenVt)
                .HasMaxLength(50)
                .HasColumnName("TenVT");

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.VatTus)
                .HasForeignKey(d => d.MaKho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VAT_TU__MaKho__5FB337D6");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.VatTus)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VAT_TU__MaLoai__5DCAEF64");

            entity.HasOne(d => d.MaTtNavigation).WithMany(p => p.VatTus)
                .HasForeignKey(d => d.MaTt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VAT_TU__MaTT__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
