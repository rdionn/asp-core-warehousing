using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services.Contracts;
using Warehouse.Ui;

namespace Warehouse.Areas.Admin.Controllers {
    [Area("Admin")]
    public class WarehouseController : Controller {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService) {
            _warehouseService = warehouseService;
        }

        [Route("admin/warehouses")]
        public async Task<IActionResult> Index(
            [FromQuery(Name = "page")] int? page,
            [FromQuery(Name = "entry")] int? entry
        ) {
            var warehouses = await _warehouseService.GetWarehouses(page ?? 1, entry ?? 10);
            return View("Index", warehouses);
        }

        [Route("admin/warehouse/{id}/products")]
        public async Task<IActionResult> GetProducts(
            [FromRoute(Name = "id")] int warehouseId
        ) {
            var products = await _warehouseService.GetProducts(warehouseId, 1, 10);
            ViewData["Products"] = products;
            return View("Products");
        }

        [Route("admin/warehouse/create")]
        [HttpGet]
        public IActionResult Create() {
            return View("Create", new WarehouseAdd());
        }

        [Route("admin/warehouse/create")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DoCreate(WarehouseAdd warehouseAdd) {
            if (ModelState.IsValid) {
                var result = await _warehouseService.AddWarehouse(warehouseAdd.ToWarehouse());

                if (result) {
                    return LocalRedirect("/admin/warehouses");
                } else {
                    ViewData["Message"] = "Can't Process Request For Now";
                }
            }

            return View("Create", warehouseAdd);
        }
    }
}