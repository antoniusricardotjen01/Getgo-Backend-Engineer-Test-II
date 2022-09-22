using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CarSharingBooking.Repositories.DBContext
{
    public partial class masterContext : DbContext
    {
        public masterContext()
        {
        }

        public masterContext(DbContextOptions<masterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarAndUser> CarAndUsers { get; set; }
        public virtual DbSet<CarUser> CarUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=ANTONIUSRICARDO;Database=master;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car", "CarBooking");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CarAndUser>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__CarAndUs__FBDF78C9494F7FBC");

                entity.ToTable("CarAndUser", "CarBooking");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarAndUsers)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__CarAndUse__CarID__216BEC9A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CarAndUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__CarAndUse__UserI__226010D3");
            });

            modelBuilder.Entity<CarUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__CarUser__1788CCACB57DE5EA");

                entity.ToTable("CarUser", "CarBooking");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserBookingName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
