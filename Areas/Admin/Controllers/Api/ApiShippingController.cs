using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services.Contracts;
using Warehouse.Ui;

namespace Warehouse.Areas.Admin.Controllers.Api
{
    public class ApiShippingController : AdminBaseController
    {
        private readonly IShippingBill _shippingBill;

        public ApiShippingController(IShippingBill shippingBill)
        {
            _shippingBill = shippingBill;
        }
        
        [Route("admin/api/shipping/add-product")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ShippingAddBillProduct shippingProduct)
        {
            if (ModelState.IsValid)
            {
                if (await _shippingBill.AddShippingProduct(shippingProduct.ShippingBillId, shippingProduct))
                {
                    return Json(new
                    {
                        Status = 200,
                        Message = "Add Product Success"
                    });
                }
            }

            var errors = ModelState.Keys.ToDictionary(key => key, key => ModelState[key].Errors.Select(e => e.ErrorMessage).ToList());
            return Json(new
            {
                Status = 422,
                Errors = errors
            });
        }
    }
}