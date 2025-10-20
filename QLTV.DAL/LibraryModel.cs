using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using QLTV.DAL.Entities;

namespace QLTV.DAL.Entities
{
    public partial class LibraryModel : DbContext
    {
        public LibraryModel()
            : base("name=LibraryModel")
        {
        }

        public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuon { get; set; }
        public virtual DbSet<DocGia> DocGia { get; set; }
        public virtual DbSet<NguoiDung> NguoiDung { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBan { get; set; }
        public virtual DbSet<PhieuMuon> PhieuMuon { get; set; }
        public virtual DbSet<Sach> Sach { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TacGia> TacGia { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietPhieuMuon>()
                .Property(e => e.MaSach)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhieuMuon>()
                .Property(e => e.TienPhat)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DocGia>()
                .Property(e => e.MaDocGia)
                .IsUnicode(false);

            modelBuilder.Entity<DocGia>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<DocGia>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<DocGia>()
                .HasMany(e => e.PhieuMuon)
                .WithRequired(e => e.DocGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NhaXuatBan>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuMuon>()
                .Property(e => e.MaDocGia)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuMuon>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuMuon>()
                .HasMany(e => e.ChiTietPhieuMuon)
                .WithRequired(e => e.PhieuMuon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.MaSach)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietPhieuMuon)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);
        }
    }
}
