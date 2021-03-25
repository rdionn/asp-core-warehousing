using System;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Models {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions options) : base(options) {}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouse { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<WarehouseAdmin> WarehouseAdmins { get; set; }
        public DbSet<ShippingBill> ShippingBills { get; set; }
        public DbSet<ShippingBillProduct> ShippingBillProducts { get; set; }
    }
}