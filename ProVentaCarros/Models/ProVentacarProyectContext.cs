﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProVentaCarros.Models;

public partial class ProVentacarProyectContext : DbContext
{
    public ProVentacarProyectContext()
    {
    }

    public ProVentacarProyectContext(DbContextOptions<ProVentacarProyectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auto> Autos { get; set; }

    public virtual DbSet<CarritoCompra> CarritoCompras { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<ItemsCarrito> ItemsCarritos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Repuesto> Repuestos { get; set; }

    public virtual DbSet<Vendedore> Vendedores { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autos__3214EC072E0E5A99");

            entity.Property(e => e.AnnoFabricacion)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Comentario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionA)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.FechaRp)
                .HasColumnType("datetime")
                .HasColumnName("FechaRP");
            entity.Property(e => e.Kilometraje).HasColumnType("decimal(6, 3)");
            entity.Property(e => e.Modelo)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Urlimagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLImagen");
            entity.Property(e => e.Urt)
                .HasColumnType("datetime")
                .HasColumnName("URT");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Autos)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Autos__IdDeparta__412EB0B6");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Autos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Autos__IdMarca__403A8C7D");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Autos)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Autos__IdVendedo__3F466844");
        });

        modelBuilder.Entity<CarritoCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarritoC__3214EC07F9037726");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.CarritoCompras)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarritoCo__IdCli__440B1D61");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC07A1F9FF31");

            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3214EC075D452AE8");

            entity.Property(e => e.Departamento1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Departamento");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleV__3214EC27879CC727");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdAutoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdAuto)
                .HasConstraintName("FK__DetalleVe__IdAut__5441852A");

            entity.HasOne(d => d.IdRepuestoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdRepuesto)
                .HasConstraintName("FK__DetalleVe__IdRep__534D60F1");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__IdVen__52593CB8");
        });

        modelBuilder.Entity<ItemsCarrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemsCar__3214EC0706E07CE3");

            entity.ToTable("ItemsCarrito");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.ItemsCarritos)
                .HasForeignKey(d => d.IdCarrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemsCarr__IdCar__4AB81AF0");

            entity.HasOne(d => d.IdRepuestoNavigation).WithMany(p => p.ItemsCarritos)
                .HasForeignKey(d => d.IdRepuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemsCarr__IdRep__4BAC3F29");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marcas__3214EC07A6ACC9E9");

            entity.Property(e => e.Marca1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Marca");
        });

        modelBuilder.Entity<Repuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Repuesto__3214EC07124B27AA");

            entity.Property(e => e.ComentarioR)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Compatiblilidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionR)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRp)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("EstadoRP");
            entity.Property(e => e.FechaRp)
                .HasColumnType("datetime")
                .HasColumnName("FechaRP");
            entity.Property(e => e.ImgProducto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreRepuesto)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Proveniencia)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Repuestos)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Repuestos__IdDep__47DBAE45");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Repuestos)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Repuestos__IdVen__46E78A0C");
        });

        modelBuilder.Entity<Vendedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendedor__3214EC07286CE21F");

            entity.Property(e => e.Apellido).HasMaxLength(25);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(20);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ventas__3214EC077CB48684");

            entity.Property(e => e.FechaExpTrajeta).HasColumnType("datetime");
            entity.Property(e => e.NombreTrajeta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RimgProducto).HasColumnName("RImgProducto");
            entity.Property(e => e.TotalPago).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__IdClient__4E88ABD4");

            entity.HasOne(d => d.IdRepuestoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdRepuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__IdRepues__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
