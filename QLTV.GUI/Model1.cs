using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=LibraryModel")
        {
        }

        public virtual DbSet<DocGia> DocGia { get; set; }
        public virtual DbSet<MuonTra> MuonTra { get; set; }
        public virtual DbSet<Sach> Sach { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocGia>()
                .HasMany(e => e.PhieuMuon)
                .WithRequired(e => e.DocGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietPhieuMuon)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheLoai>()
                .HasMany(e => e.Sach)
                .WithRequired(e => e.TheLoai)
                .WillCascadeOnDelete(false);
        }
    }
}
