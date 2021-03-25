using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models {
    [Table("shipping_bills")]
    public class ShippingBill {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("bill_invoice")]
        public string BillInvoice { get; set;  }

        [Column("warehouse_id")]
        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        public List<ShippingBillProduct> Products { get; set; }
        public Warehouse Warehouse { get; set;  }
    }
}