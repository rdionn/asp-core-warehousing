using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services;

namespace Warehouse.Controllers {
    public class OngkirController : Controller {
        private readonly RajaOngkirService  _ongkirService;

        public OngkirController(RajaOngkirService rajaService) {
            _ongkirService = rajaService;
        }

        [Route("api/ongkir")]
        public async Task<IActionResult> Index() {
            var result = await _ongkirService.GetProvices();
            return Json(result);
        }
    }
}