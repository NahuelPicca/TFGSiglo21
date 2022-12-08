﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RossiEventos;

#nullable disable

namespace RossiEventos.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221208211229_AddComprobanteEntrega")]
    partial class AddComprobanteEntrega
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RossiEventos.Entidades.AsignacionVehicTransp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransportitaId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransportitaId");

                    b.HasIndex("VehiculoId");

                    b.ToTable("AsignacionVehicTransp");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Calidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Calidad");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("RossiEventos.Entidades.ComprobanteEntrega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<int>("NroDni")
                        .HasColumnType("int");

                    b.Property<string>("TipoDni")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ComprobanteEntrega");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AsignacionId")
                        .HasColumnType("int");

                    b.Property<string>("Factura")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPedido")
                        .HasColumnType("datetime2");

                    b.Property<string>("NroPedido")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar");

                    b.Property<bool>("Pagado")
                        .HasColumnType("bit");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AsignacionId");

                    b.HasIndex("Factura")
                        .IsUnique();

                    b.HasIndex("NroPedido")
                        .IsUnique();

                    b.HasIndex("ReservaId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("CodigoPostal")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar");

                    b.Property<string>("Cuit")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Localidad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<int>("NroDni")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar");

                    b.Property<string>("TipoDni")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Cuit")
                        .IsUnique();

                    b.HasIndex("NroDni")
                        .IsUnique();

                    b.ToTable("Persona");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("RossiEventos.Entidades.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Año")
                        .HasColumnType("datetime2");

                    b.Property<int>("CalidadId")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Habilitado")
                        .HasColumnType("bit");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CalidadId");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("TipoId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("RossiEventos.Entidades.RenglonDeReserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<decimal>("CostoTotal")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.Property<decimal>("CostoUnitario")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("ReservaId");

                    b.ToTable("RenglonDeReserva");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadPersonas")
                        .HasColumnType("int");

                    b.Property<string>("CodigoPostal")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar");

                    b.Property<string>("DireccionEvento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaEvento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaReserva")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocalidadEvento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("NroReserva")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar");

                    b.Property<string>("ProvinciaEvento")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("NroReserva", "UsuarioId")
                        .IsUnique();

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("RossiEventos.Entidades.TipoProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("TipoProducto");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaInsercion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencPoliza")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Habilitado")
                        .HasColumnType("bit");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("NroPoliza")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("Patente")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar");

                    b.Property<int>("TitularId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioInserto")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("NroPoliza")
                        .IsUnique();

                    b.HasIndex("Patente")
                        .IsUnique();

                    b.HasIndex("TitularId");

                    b.ToTable("Vehiculo");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Titular", b =>
                {
                    b.HasBaseType("RossiEventos.Entidades.Persona");

                    b.ToTable("Titular");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Transportista", b =>
                {
                    b.HasBaseType("RossiEventos.Entidades.Persona");

                    b.Property<DateTime>("FechaVencLicencia")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Habilitado")
                        .HasColumnType("bit");

                    b.Property<string>("Licencia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.ToTable("Transportista");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.HasBaseType("RossiEventos.Entidades.Persona");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.Property<DateTime?>("FechaBaja")
                        .HasColumnType("datetime2");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("RossiEventos.Entidades.AsignacionVehicTransp", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Transportista", "Transportista")
                        .WithMany("AsignacionVehicTrans")
                        .HasForeignKey("TransportitaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RossiEventos.Entidades.Vehiculo", "Vehiculo")
                        .WithMany("AsignacionVehicTrans")
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Transportista");

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Pedido", b =>
                {
                    b.HasOne("RossiEventos.Entidades.AsignacionVehicTransp", "Asignacion")
                        .WithMany()
                        .HasForeignKey("AsignacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RossiEventos.Entidades.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asignacion");

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Producto", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Calidad", "Calidad")
                        .WithMany()
                        .HasForeignKey("CalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RossiEventos.Entidades.TipoProducto", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calidad");

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("RossiEventos.Entidades.RenglonDeReserva", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RossiEventos.Entidades.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Reserva", b =>
                {
                    b.HasOne("Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RossiEventos.Entidades.TipoProducto", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Vehiculo", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Titular", "Titular")
                        .WithMany("Vehiculos")
                        .HasForeignKey("TitularId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Titular");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Titular", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Persona", null)
                        .WithOne()
                        .HasForeignKey("RossiEventos.Entidades.Titular", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RossiEventos.Entidades.Transportista", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Persona", null)
                        .WithOne()
                        .HasForeignKey("RossiEventos.Entidades.Transportista", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.HasOne("RossiEventos.Entidades.Persona", null)
                        .WithOne()
                        .HasForeignKey("Usuario", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RossiEventos.Entidades.Vehiculo", b =>
                {
                    b.Navigation("AsignacionVehicTrans");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Titular", b =>
                {
                    b.Navigation("Vehiculos");
                });

            modelBuilder.Entity("RossiEventos.Entidades.Transportista", b =>
                {
                    b.Navigation("AsignacionVehicTrans");
                });
#pragma warning restore 612, 618
        }
    }
}
