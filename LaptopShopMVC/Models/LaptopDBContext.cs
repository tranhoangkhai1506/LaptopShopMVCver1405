using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LaptopShopMVC.Models
{
    public partial class LaptopDBContext : DbContext
    {
        public LaptopDBContext()
            : base("name=LaptopDBContext")
        {
        }

        public virtual DbSet<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public virtual DbSet<CHITIETPHIEUNHAPHANG> CHITIETPHIEUNHAPHANGs { get; set; }
        public virtual DbSet<DANHGIA> DANHGIAs { get; set; }
        public virtual DbSet<DANHMUC> DANHMUCs { get; set; }
        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }
        public virtual DbSet<PHIEUBAOHANH> PHIEUBAOHANHs { get; set; }
        public virtual DbSet<PHIEUNHAPHANG> PHIEUNHAPHANGs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<TAIKHOANKHACHHANG> TAIKHOANKHACHHANGs { get; set; }
        public virtual DbSet<TAIKHOANQUANTRI> TAIKHOANQUANTRIs { get; set; }
        public virtual DbSet<THUONGHIEU> THUONGHIEUx { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CHITIETDONHANG>()
                .Property(e => e.GIA)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CHITIETPHIEUNHAPHANG>()
                .Property(e => e.GIA)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DONHANG>()
                .Property(e => e.TONGTIEN)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DONHANG>()
                .HasMany(e => e.CHITIETDONHANGs)
                .WithRequired(e => e.DONHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<PHIEUNHAPHANG>()
                .Property(e => e.TONGTIEN)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PHIEUNHAPHANG>()
                .HasMany(e => e.CHITIETPHIEUNHAPHANGs)
                .WithRequired(e => e.PHIEUNHAPHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.HINH)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.GIABAN)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.GIANHAP)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETDONHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETPHIEUNHAPHANGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);
        }
    }
}
