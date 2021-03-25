using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models {
    [Table("shipping_bills_products")]
    public class ShippingBillProduct {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("shipping_bill_id")]
        [ForeignKey("Shipping")]
        public int ShippingBillId { get; set; }

        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Column("qty")]
        public int Qty { get; set; }

        public ShippingBill Bill { get; set; }
        public Product Product { get; set; }
    }
}