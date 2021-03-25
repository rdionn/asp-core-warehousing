using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models {
    [Table("product_warehouse")]
    public class ProductWarehouse {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Column("warehouse_id")]
        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}