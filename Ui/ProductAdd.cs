using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Warehouse.Models;

namespace Warehouse.Ui {
    public class ProductAdd {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sku Is Needed", AllowEmptyStrings = false)]
        public String Sku { get; set; }

        [Required(ErrorMessage = "Name Is Needed", AllowEmptyStrings = false)]
        public String Name { get; set; }

        [Required(ErrorMessage = "Barcode Is Needed", AllowEmptyStrings = false)]
        public String Barcode { get; set; }

        [Required(ErrorMessage = "Baseprice Is Needed", AllowEmptyStrings = false)]
        [Range(0, Double.MaxValue, ErrorMessage = "Minimum Price Is 0")]
        public int BasePrice { get; set; }

        [Required(ErrorMessage = "Weight Is Needed", AllowEmptyStrings = false)]
        [Range(0, Double.MaxValue, ErrorMessage = "Minimum Weight Is 0")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Status Is Needed", AllowEmptyStrings = false)]
        public String Status { get; set; }

        public List<IFormFile> Images { get; set; }

        public Product ToProduct() {
            return new Product() {
                Sku = Sku,
                Barcode = Barcode,
                BasePrice = BasePrice,
                Name = Name,
                Weight = Weight,
                Status = Status
            };
        }

        public static ProductAdd FromProduct(Product p) {
            return new ProductAdd() {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku,
                Weight = p.Weight,
                BasePrice = p.BasePrice,
                Status = p.Status
            };
        }
    }
}