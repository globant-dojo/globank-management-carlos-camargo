using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankDojo.Models.Data
{
    public partial class BankDojoDBContext : DbContext
    {
        public BankDojoDBContext()
        {
        }

        public BankDojoDBContext(DbContextOptions<BankDojoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCliente> TblClientes { get; set; } = null!;
        public virtual DbSet<TblCuentum> TblCuenta { get; set; } = null!;
        public virtual DbSet<TblMovimiento> TblMovimientos { get; set; } = null!;
        public virtual DbSet<TblPersona> TblPersonas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("tblCliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(300)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdPersonaFk).HasColumnName("idPersonaFK");

                entity.HasOne(d => d.IdPersonaFkNavigation)
                    .WithMany(p => p.TblClientes)
                    .HasForeignKey(d => d.IdPersonaFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCliente_tblPersona1");
            });

            modelBuilder.Entity<TblCuentum>(entity =>
            {
                entity.HasKey(e => e.IdCuenta);

                entity.ToTable("tblCuenta");

                entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdClienteFk).HasColumnName("idClienteFK");

                entity.Property(e => e.NumeroCuenta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("numeroCuenta");

                entity.Property(e => e.SaldoInicial)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("saldoInicial");

                entity.Property(e => e.TipoCuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipoCuenta");

                entity.HasOne(d => d.IdClienteFkNavigation)
                    .WithMany(p => p.TblCuenta)
                    .HasForeignKey(d => d.IdClienteFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCuenta_tblCliente1");
            });

            modelBuilder.Entity<TblMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento);

                entity.ToTable("tblMovimiento");

                entity.Property(e => e.IdMovimiento).HasColumnName("idMovimiento");

                entity.Property(e => e.FechaMovimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaMovimiento");

                entity.Property(e => e.IdCuentaFk).HasColumnName("idCuentaFK");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("saldo");

                entity.Property(e => e.TipoMovimiento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipoMovimiento");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdCuentaFkNavigation)
                    .WithMany(p => p.TblMovimientos)
                    .HasForeignKey(d => d.IdCuentaFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMovimiento_tblCuenta1");
            });

            modelBuilder.Entity<TblPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("tblPersona");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Genero)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(500)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(1000)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
