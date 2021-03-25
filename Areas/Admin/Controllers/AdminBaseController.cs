using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Warehouse.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class AdminBaseController : Controller {
    }
}