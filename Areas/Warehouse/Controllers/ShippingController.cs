using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services.Contracts;

namespace Warehouse.Areas.Warehouse.Controllers
{
    public class ShippingController : WarehouseBaseController
    {
        private readonly IShippingBill _shippingBill;

        public ShippingController(IShippingBill shippingBill)
        {
            _shippingBill = shippingBill;
        }

        [Route("warehouse/shippings")]
        public async Task<IActionResult> Index([FromQuery(Name = "page")] int? page, [FromQuery(Name = "entry")] int? entry)
        {
            var warehouse = HttpContext.Items["WarehouseContext"] as Models.Warehouse;
            var shippingBills = await _shippingBill.GetShippingBillWarehouse(warehouse.Id, page ?? 1, entry ?? 10);
            return View("Index", shippingBills);
        }
    }
}