using System;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Ui
{
    public class AddShippingProduct
    {
        [Required(ErrorMessage = "Shipping Id Required", AllowEmptyStrings = false)]
        public int ShippingId { get; set; }
        
        [Required(ErrorMessage = "Id Product Required", AllowEmptyStrings = false)]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Qty Is Required", AllowEmptyStrings = false)]
        [Range(0, Double.MaxValue, ErrorMessage = "Qty Min Is 0")]
        public int Qty { get; set; }
    }
}