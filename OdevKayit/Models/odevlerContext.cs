using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OdevKayit.Models
{
    public partial class odevlerContext : DbContext
    {
        public odevlerContext()
        {
        }

        public odevlerContext(DbContextOptions<odevlerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Odev> Odev { get; set; }
        public virtual DbSet<Ogrenci> Ogrenci { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;user id=root;password=12345;database=odevler;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Odev>(entity =>
            {
                entity.ToTable("odev");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.ToTable("ogrenci");

                entity.HasIndex(e => e.OdevId)
                    .HasName("FK_OdevId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Isim)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OdevId).HasColumnName("OdevID");

                entity.Property(e => e.Soyisim).HasMaxLength(100);

                entity.HasOne(d => d.Odev)
                    .WithMany(p => p.Ogrenci)
                    .HasForeignKey(d => d.OdevId)
                    .HasConstraintName("FK_OdevId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
