using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services.Contracts;
using Warehouse.Ui;

namespace Warehouse.Areas.Admin.Controllers {
    public class ShippingController : AdminBaseController
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IShippingBill _shippingBill;

        public ShippingController(IWarehouseService warehouseService, IShippingBill shippingBill)
        {
            _warehouseService = warehouseService;
            _shippingBill = shippingBill;
        }
        
        [Route("admin/shipping/create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Warehouses"] = await _warehouseService.GetAllWarehouses();
            return View("Create");
        }

        [Route("admin/shipping/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [FromForm(Name = "Warehouse")] int? warehouse
        )
        {
            ViewData["Warehouses"] = await _warehouseService.GetAllWarehouses();
            
            if (warehouse == null)
            {
                ViewData["ErrorMessage"] = "Invalid Warehouse";
            }
            else
            {
                var shippingBillId = await _shippingBill.CreateShippingBill(warehouse.Value);

                if (shippingBillId != -1)
                {
                    return LocalRedirect($"/admin/shipping/detail/{shippingBillId}");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Can't Create Shipping Bill For Now";
                }
            }
            
            return View("Create");
        }

        [Route("admin/shipping/detail/{shippingId}")]
        public async Task<IActionResult> Detail(
            [FromRoute(Name = "shippingId")] int? shippingId,
            [FromQuery(Name = "page")] int? page,
            [FromQuery(Name = "entry")] int? entry
            )
        {
            if (shippingId != null)
            {
                var shippingBill = await _shippingBill.GetShippingBill(shippingId.Value);
                var shippingProducts = await _shippingBill.GetShippingProducts(shippingId.Value, page ?? 1, entry ?? 10);
                var addShippingProduct = new AddShippingProduct();

                ViewData["Products"] = shippingProducts;
                return View("Detail", shippingBill);
            }

            return LocalRedirect("/error/not-found");
        }
    }
}