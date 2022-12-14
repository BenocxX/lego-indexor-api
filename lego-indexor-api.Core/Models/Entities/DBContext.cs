using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace lego_indexor_api.Core.Models.Entities
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Connection> Connections { get; set; } = null!;
        public virtual DbSet<Piece> Pieces { get; set; } = null!;
        public virtual DbSet<Raspberrypi> Raspberrypis { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=lego-indexor;Username=dev;Password=dev");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>(entity =>
            {
                entity.ToTable("connection");

                entity.HasIndex(e => e.Id, "connection_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Token, "connection_token_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Connections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("connection_user_id__fk");
            });

            modelBuilder.Entity<Piece>(entity =>
            {
                entity.ToTable("piece");

                entity.HasIndex(e => e.Id, "lego_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('lego_id_seq'::regclass)");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pieces)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("piece_user_id_fk");
            });

            modelBuilder.Entity<Raspberrypi>(entity =>
            {
                entity.ToTable("raspberrypi");

                entity.HasIndex(e => e.Id, "raspberrypi_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.MacAddress, "raspberrypi_mac_address_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(6)
                    .HasColumnName("code");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(15)
                    .HasColumnName("ip_address");

                entity.Property(e => e.MacAddress)
                    .HasMaxLength(17)
                    .HasColumnName("mac_address");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Raspberrypis)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("raspberrypi_user_id_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Id, "user_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "user_username_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
