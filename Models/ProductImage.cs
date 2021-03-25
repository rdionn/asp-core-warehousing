using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models
{
    [Table("product_image")]
    public class ProductImage {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Column("filename")]
        public String Filename { get; set; }

        public Product Product { get; set; }
    }
}