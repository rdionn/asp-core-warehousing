using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Warehouse.Models {
    [Table("products")]
    public class Product {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("sku")]
        public String Sku { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("barcode")]
        public String Barcode { get; set; }

        [Column("base_price")]
        public int BasePrice { get; set; }

        [Column("weight")]
        public int Weight { get; set; }

        [Column("status")]
        public String Status { get; set; }

        public ICollection<ProductImage> Images { get; set; }
    }
}