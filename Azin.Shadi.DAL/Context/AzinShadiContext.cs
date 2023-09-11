using Azin.Shadi.DAL.Entities.Comment;
using Azin.Shadi.DAL.Entities.Discount;
using Azin.Shadi.DAL.Entities.Forward;
using Azin.Shadi.DAL.Entities.Order;
using Azin.Shadi.DAL.Entities.Permission;
using Azin.Shadi.DAL.Entities.Product;
using Azin.Shadi.DAL.Entities.Transaction;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Context
{
    public class AzinShadiContext : DbContext
    {
        public AzinShadiContext(DbContextOptions<AzinShadiContext> options) : base(options) { }

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        #region Product

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserDiscounts> UserDiscounts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        #endregion

        #region Transaction
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        #endregion

        #region Permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        #endregion

        #region Forward
        public DbSet<Forward> Forwards { get; set; }
        #endregion
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(t => !t.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(t => !t.IsDelete);

            //modelBuilder.Entity<Order>().HasOne(t => t.OrderStatus).WithMany(t => t.Orders).HasForeignKey(t => t.StatusId);

            #region Default Data
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Name = "یاسین عابدینی",
                Username = "2f649a9139",
                ActivationCode = "2b7528060679435088ba01f27e4eb63f",
                Email = "yasinabedini1384@gmail.com",
                EmailConfrimation = true,
                IsActive = true,
                IsDelete = false,
                Password = "3F-DF-50-75-BD-FF-CD-76-85-D8-46-89-11-FD-CD-C1",
                ProfileName = "ce7ca676827f42e485b9167a86f19a6f.jpg",
                RegisterDate = DateTime.Now,
            });

            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 1,
                PermissionTitle = "پنل مدیریت"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 2,
                PermissionTitle = "مدیریت کاربران",
                ParentId = 1
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 3,
                PermissionTitle = "اضافه کردن کاربر",
                ParentId = 2

            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 4,
                PermissionTitle = "حذف کاربر",
                ParentId = 2
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {

                Id = 5,
                PermissionTitle = "ویرایش کاربر",
                ParentId = 2
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 6,
                PermissionTitle = "مدیریت نقش ها",
                ParentId = 1
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 7,
                PermissionTitle = "اضافه کردن نقش",
                ParentId = 6
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 8,
                PermissionTitle = "حذف نقش",
                ParentId = 6
            });
            modelBuilder.Entity<Permission>().HasData(new Permission
            {
                Id = 9,
                PermissionTitle = "ویرایش نقش",
                ParentId = 6
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = 1,
                Title = "مدیر سایت"
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = 2,
                Title = "فروشنده"
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = 3,
                Title = "کاربر عادی"
            });

            modelBuilder.Entity<TransactionType>().HasData(new TransactionType
            {
                TransactionTypeId = 1,
                InvoiceTypeTitle = "OnlinePayment"
            });
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType
            {
                TransactionTypeId = 2,
                InvoiceTypeTitle = "WalletPayment"
            });
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType
            {
                TransactionTypeId = 3,
                InvoiceTypeTitle = "WalletRecharge"
            });

            modelBuilder.Entity<ProductStatus>().HasData(new ProductStatus
            {
                Id = 1,
                Title = "موجود در انبار"
            });
            modelBuilder.Entity<ProductStatus>().HasData(new ProductStatus
            {
                Id = 2,
                Title = "اتمام موجودی"
            });



            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
