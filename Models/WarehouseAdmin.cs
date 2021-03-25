using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models {
    [Table("warehouse_admins")]
    public class WarehouseAdmin {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("warehouse_id")]
        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }

        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("status")]
        public String Status { get; set; }

        public Warehouse Warehouse { get; set; }
        public User User { get; set; }
    }
}