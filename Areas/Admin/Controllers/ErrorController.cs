using System;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ErrorController : Controller {
        [Route("admin/error/{errorType}")]
        public IActionResult Error(
            [FromRoute(Name = "errorType")]
            String errorType
        ) {
            return View("Error");
        }
    }
}