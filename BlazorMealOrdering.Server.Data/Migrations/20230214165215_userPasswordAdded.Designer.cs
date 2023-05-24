﻿// <auto-generated />
using System;
using BlazorMealOrdering.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlazorMealOrdering.Server.Data.Migrations
{
    [DbContext(typeof(MealOrderinDbContext))]
    [Migration("20230214165215_userPasswordAdded")]
    partial class userPasswordAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("create_user_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expire_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid")
                        .HasColumnName("supplier_id");

                    b.HasKey("Id")
                        .HasName("pk_order_id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("SupplierId");

                    b.ToTable("orders", "public");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("create_user_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.HasKey("Id")
                        .HasName("pk_orderItem_id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("order_items", "public");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("WebURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying")
                        .HasColumnName("web_url");

                    b.HasKey("Id")
                        .HasName("pk_supplier_id");

                    b.ToTable("suppliers", "public");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_user_id");

                    b.ToTable("users", "public");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.Order", b =>
                {
                    b.HasOne("BlazorMealOrdering.Server.Data.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_order_id");

                    b.HasOne("BlazorMealOrdering.Server.Data.Models.Supplier", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_order_id");

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.OrderItem", b =>
                {
                    b.HasOne("BlazorMealOrdering.Server.Data.Models.User", "User")
                        .WithMany("OrderItems")
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_orderitem_id");

                    b.HasOne("BlazorMealOrdering.Server.Data.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_orderitem_id");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.Supplier", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BlazorMealOrdering.Server.Data.Models.User", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
