﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebSite.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyWebSite.Migrations
{
    [DbContext(typeof(NailStoreContext))]
    partial class NailStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyWebSite.Models.Categories", b =>
                {
                    b.Property<int>("CategoriesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoriesId"));

                    b.Property<string[]>("categories")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("CategoriesId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MyWebSite.Models.NailItem", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<float>("CustomerLength")
                        .HasColumnType("real");

                    b.Property<float>("CustomerWidth")
                        .HasColumnType("real");

                    b.Property<string>("PressOnLength")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PressOnStyle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PurchasedOrderOrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Sets")
                        .HasColumnType("integer");

                    b.HasKey("ProductId");

                    b.HasIndex("PurchasedOrderOrderId");

                    b.ToTable("NailItem");
                });

            modelBuilder.Entity("MyWebSite.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoriesId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string[]>("Photos")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("TotalSold")
                        .HasColumnType("integer");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MyWebSite.Models.PurchasedOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PurchasedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TotalPrice")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MyWebSite.Models.NailItem", b =>
                {
                    b.HasOne("MyWebSite.Models.PurchasedOrder", null)
                        .WithMany("NailsOrdered")
                        .HasForeignKey("PurchasedOrderOrderId");
                });

            modelBuilder.Entity("MyWebSite.Models.Product", b =>
                {
                    b.HasOne("MyWebSite.Models.Categories", "Categories")
                        .WithMany()
                        .HasForeignKey("CategoriesId");

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("MyWebSite.Models.PurchasedOrder", b =>
                {
                    b.Navigation("NailsOrdered");
                });
#pragma warning restore 612, 618
        }
    }
}
