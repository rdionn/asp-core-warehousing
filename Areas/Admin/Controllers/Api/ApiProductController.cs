using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Services.Contracts;

namespace Warehouse.Areas.Admin.Controllers.Api
{
    public class ApiProductController : AdminBaseController
    {
        private readonly IProductService _productService;

        public ApiProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [Route("admin/api/product/search-product")]
        [HttpGet]
        public async Task<IActionResult> FindProduct(
            [FromQuery(Name = "q")] string? query
        )
        {
            if (!String.IsNullOrEmpty(query))
            {
                var products = await _productService.FindProducts(query);
                return Json(products.Select(p => new {Value = p.Id, Text = p.Name}));
            }
            
            return Json(new List<String>());
        }
    }
}