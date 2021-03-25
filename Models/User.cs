using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Warehouse.Models
{
    [Table("user")]
    public class User : IdentityUser<int> {
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [Column("username")]
        public override string UserName { get; set; }

        [NotMapped]
        public override string NormalizedUserName { get; set; }

        [Column("email")]
        public override string Email { get; set; }

        [Column("status")]
        public String Status { get; set; }

        [NotMapped]
        public override string NormalizedEmail { get; set; }

        [NotMapped]
        public override bool EmailConfirmed { get; set; }

        [Column("password")]
        public override string PasswordHash { get; set; }

        [NotMapped]
        public override string SecurityStamp { get; set; }

        [NotMapped]
        public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Column("phone")]
        public override string PhoneNumber { get; set; }

        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }

        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }

        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }

        [NotMapped]
        public override bool LockoutEnabled { get; set; }

        [NotMapped]
        public override int AccessFailedCount { get; set; }
    }
}