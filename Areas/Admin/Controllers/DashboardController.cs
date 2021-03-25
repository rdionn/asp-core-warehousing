using System;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Areas.Admin.Controllers {
    [Area("Admin")]
    public class DashboardController : AdminBaseController {
        [Route("admin/")]
        public IActionResult Index() {
            return View("Index");
        }
    }
}