using System;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Areas.Warehouse.Controllers
{
    public class WarehouseController : WarehouseBaseController
    {
        [Route("warehouse")]
        public IActionResult Index() {
            return View("Index");
        }
    }
}