using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Areas.Member.Controllers {
    [Area("Member")]
    [Authorize(Roles = "MEMBER")]
    public class MemberController : Controller {
        [Route("member")]
        public IActionResult Index() {
            return View("Index");
        }
    }
}