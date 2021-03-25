using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Filters;

namespace Warehouse.Areas.Warehouse.Controllers {
    [Area("Warehouse")]
    [Authorize(Roles = "WAREHOUSE_ADMIN")]
    [TypeFilter(typeof(WarehouseContext))]
    public class WarehouseBaseController : Controller {
    }
}