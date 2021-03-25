using System;
using System.ComponentModel.DataAnnotations;
using Warehouse.Models;

namespace Warehouse.Ui {
    public class ShippingAddBillProduct {
        [Required(ErrorMessage = "Shipping Bill Id Required", AllowEmptyStrings = false)]
        public int ShippingBillId { get; set; }

        [Required(ErrorMessage = "Product Id Required", AllowEmptyStrings = false)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Qty Is Required")]
        [Range(1, Double.MaxValue, ErrorMessage = "Minimum Qty Is 1")]
        public int Qty { get; set; }

        public ShippingBillProduct ToShippingBillProduct() {
            return new ShippingBillProduct() {
                ShippingBillId = ShippingBillId,
                ProductId = ProductId,
                Qty = Qty
            };
        }
    }
}