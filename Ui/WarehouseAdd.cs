using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Ui
{
    public class WarehouseAdd {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required", AllowEmptyStrings = false)]
        public String Name { get; set; }

        [Required(ErrorMessage = "Address Is Required", AllowEmptyStrings = false)]
        public String Address { get; set; }

        [Required(ErrorMessage = "Email Is Required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Email Must Valid")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Phone Is Required", AllowEmptyStrings = false)]
        public String Phone { get; set; }

        public String Status { get; set; } = "PUBLISH";

        public Warehouse.Models.Warehouse ToWarehouse() {
            return new Warehouse.Models.Warehouse() {
                Name = Name,
                Email = Email,
                Address = Address,
                Phone = Phone,
                Status = Status
            };
        }

        public static WarehouseAdd FromWarehouse(Warehouse.Models.Warehouse warehouse) {
            return new WarehouseAdd() {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Email = warehouse.Email,
                Phone = warehouse.Phone,
                Address = warehouse.Address,
                Status = warehouse.Status
            };
        }
    }
}