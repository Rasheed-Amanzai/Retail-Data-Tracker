﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Retail_Data_Tracker.Data;

#nullable disable

namespace Retail_Data_Tracker.Migrations
{
    [DbContext(typeof(Retail_Data_TrackerContext))]
    [Migration("20231209071815_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Retail_Data_Tracker.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("BuyCost")
                        .HasColumnType("float");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit");

                    b.Property<string>("ItemDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("SellCost")
                        .HasColumnType("float");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderClientId")
                        .HasColumnType("int");

                    b.Property<int>("ShippingDetailsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderClientId");

                    b.HasIndex("ShippingDetailsId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Shipping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArrivalDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("ShippingDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("clientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("clientId");

                    b.ToTable("Shipping");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Item", b =>
                {
                    b.HasOne("Retail_Data_Tracker.Models.Client", null)
                        .WithMany("ClientOrder")
                        .HasForeignKey("ClientId");

                    b.HasOne("Retail_Data_Tracker.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("Retail_Data_Tracker.Models.Supplier", "ItemSupplier")
                        .WithMany("SupplierInventory")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemSupplier");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Order", b =>
                {
                    b.HasOne("Retail_Data_Tracker.Models.Client", "OrderClient")
                        .WithMany()
                        .HasForeignKey("OrderClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Retail_Data_Tracker.Models.Shipping", "ShippingDetails")
                        .WithMany()
                        .HasForeignKey("ShippingDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderClient");

                    b.Navigation("ShippingDetails");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Shipping", b =>
                {
                    b.HasOne("Retail_Data_Tracker.Models.Client", "client")
                        .WithMany()
                        .HasForeignKey("clientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Client", b =>
                {
                    b.Navigation("ClientOrder");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Retail_Data_Tracker.Models.Supplier", b =>
                {
                    b.Navigation("SupplierInventory");
                });
#pragma warning restore 612, 618
        }
    }
}
