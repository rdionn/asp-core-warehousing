using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models {
    [Table("warehouse")]
    public class Warehouse {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("address")]
        public String Address { get; set; }

        [Column("phone")]
        public String Phone { get; set; }

        [Column("email")]
        public String Email { get; set; }

        [Column("status")]
        public String Status { get; set; }

        public ICollection<ProductWarehouse> Products { get; set; }
    }
}