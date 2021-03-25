using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Warehouse.Models
{
    [Table("roles")]
    public class Role : IdentityRole<int> {
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("name")]
        public override string Name { get; set; }

        [NotMapped]
        public virtual string NormalizedName { get; set; }

        [NotMapped]
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public User User { get; set; }
    }
}