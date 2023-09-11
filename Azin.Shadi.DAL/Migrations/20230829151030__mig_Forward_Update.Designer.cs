﻿// <auto-generated />
using System;
using Azin.Shadi.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Azin.Shadi.DAL.Migrations
{
    [DbContext(typeof(AzinShadiContext))]
    [Migration("20230829151030__mig_Forward_Update")]
    partial class _mig_Forward_Update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Forward.Forward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsForward")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("TrackingCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Forwards");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Order.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ForwardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinaly")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPay")
                        .HasColumnType("bit");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("SumPrice")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ForwardId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Order.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SumPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Order.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Permission.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PermissionTitle = "پنل مدیریت"
                        },
                        new
                        {
                            Id = 2,
                            ParentId = 1,
                            PermissionTitle = "مدیریت کاربران"
                        },
                        new
                        {
                            Id = 3,
                            ParentId = 2,
                            PermissionTitle = "اضافه کردن کاربر"
                        },
                        new
                        {
                            Id = 4,
                            ParentId = 2,
                            PermissionTitle = "حذف کاربر"
                        },
                        new
                        {
                            Id = 5,
                            ParentId = 2,
                            PermissionTitle = "ویرایش کاربر"
                        },
                        new
                        {
                            Id = 6,
                            ParentId = 1,
                            PermissionTitle = "مدیریت نقش ها"
                        },
                        new
                        {
                            Id = 7,
                            ParentId = 6,
                            PermissionTitle = "اضافه کردن نقش"
                        },
                        new
                        {
                            Id = 8,
                            ParentId = 6,
                            PermissionTitle = "حذف نقش"
                        },
                        new
                        {
                            Id = 9,
                            ParentId = 6,
                            PermissionTitle = "ویرایش نقش"
                        });
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Permission.RolePermission", b =>
                {
                    b.Property<int>("RP_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RP_Id"));

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("RP_Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PictureName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.HasIndex("StatusId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.ProductStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ProductStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "موجود در انبار"
                        },
                        new
                        {
                            Id = 2,
                            Title = "اتمام موجودی"
                        });
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Transaction.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PayDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("TransactionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Transaction.TransactionType", b =>
                {
                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceTypeTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("TransactionTypeId");

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            TransactionTypeId = 1,
                            InvoiceTypeTitle = "OnlinePayment"
                        },
                        new
                        {
                            TransactionTypeId = 2,
                            InvoiceTypeTitle = "WalletPayment"
                        },
                        new
                        {
                            TransactionTypeId = 3,
                            InvoiceTypeTitle = "WalletRecharge"
                        });
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.User.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            IsDelete = false,
                            Title = "مدیر سایت"
                        },
                        new
                        {
                            RoleId = 2,
                            IsDelete = false,
                            Title = "فروشنده"
                        },
                        new
                        {
                            RoleId = 3,
                            IsDelete = false,
                            Title = "کاربر عادی"
                        });
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<bool>("EmailConfrimation")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            ActivationCode = "2b7528060679435088ba01f27e4eb63f",
                            Email = "yasinabedini1384@gmail.com",
                            EmailConfrimation = true,
                            IsActive = true,
                            IsDelete = false,
                            Name = "یاسین عابدینی",
                            Password = "3F-DF-50-75-BD-FF-CD-76-85-D8-46-89-11-FD-CD-C1",
                            ProfileName = "ce7ca676827f42e485b9167a86f19a6f.jpg",
                            RegisterDate = new DateTime(2023, 8, 29, 18, 40, 30, 420, DateTimeKind.Local).AddTicks(2175),
                            Username = "2f649a9139"
                        });
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.User.UserRole", b =>
                {
                    b.Property<int>("UR_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UR_Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UR_Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Forward.Forward", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Order.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Order.Order", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Forward.Forward", "Forward")
                        .WithMany()
                        .HasForeignKey("ForwardId");

                    b.HasOne("Azin.Shadi.DAL.Entities.Order.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Azin.Shadi.DAL.Entities.User.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Forward");

                    b.Navigation("OrderStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Order.OrderLine", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Order.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Azin.Shadi.DAL.Entities.Product.Product", "Product")
                        .WithMany("OrderLines")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Permission.Permission", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Permission.Permission", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Permission.RolePermission", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Permission.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Azin.Shadi.DAL.Entities.User.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.Product", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Product.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Azin.Shadi.DAL.Entities.Product.ProductStatus", "ProductStatus")
                        .WithMany("Products")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductGroup");

                    b.Navigation("ProductStatus");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.ProductGroup", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Product.ProductGroup", null)
                        .WithMany("ProductGroups")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Transaction.Transaction", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.Transaction.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Azin.Shadi.DAL.Entities.User.User", "Related_User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Related_User");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.User.UserRole", b =>
                {
                    b.HasOne("Azin.Shadi.DAL.Entities.User.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Azin.Shadi.DAL.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Order.Order", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Permission.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.Product", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.ProductGroup", b =>
                {
                    b.Navigation("ProductGroups");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.Product.ProductStatus", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.User.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Azin.Shadi.DAL.Entities.User.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
