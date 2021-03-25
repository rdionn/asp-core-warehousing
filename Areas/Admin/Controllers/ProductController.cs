using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Warehouse.Services.Contracts;
using Warehouse.Ui;

namespace Warehouse.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class ProductController : AdminBaseController {
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;
        private readonly IUploadService _uploadService;

        public ProductController(IProductService productService, IUploadService uploadService, IWarehouseService warehouseService) {
            _productService = productService;
            _uploadService = uploadService;
            _warehouseService = warehouseService;
        }

        [Route("admin/products")]
        public async Task<IActionResult> Index(
            [FromQuery(Name = "page")] int? page,
            [FromQuery(Name = "entry")] int? entry,
            [FromQuery(Name = "name")] String? name,
            [FromQuery(Name = "status")] String? status
        ) {
            var products = await _productService.GetProducts(new ProductFilter() {
                Name = name,
                Status = status ?? "PUBLISH"
            } ,page ?? 1, entry ?? 10);
            ViewData["Products"] = products;
            
            return View("Index");
        }

        [Route("admin/product/detail/{productId}")]
        public async Task<IActionResult> Detail(
            [FromRoute(Name = "productId")] int productId,
            [FromQuery(Name = "page")] int? page,
            [FromQuery(Name = "entry")] int? entry
        ) {
            var product = await _productService.GetProduct(productId);

            if (product == null) {
                return LocalRedirect("/admin/error/not-found");
            }

            ViewData["Locations"] = await _warehouseService.GetProductLocations(productId, page ?? 1, entry ?? 10);
            return View("Detail", product);
        }

        [Route("admin/product/create")]
        [HttpGet]
        public IActionResult CreateProduct() {
            return View("Create", new ProductAdd());
        }

        [Route("admin/product/create")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductAdd addProduct) {
            if (ModelState.IsValid) {
                var skuBarcodeValid = await _productService.CheckProduct(addProduct.Sku, addProduct.Barcode);

                if (skuBarcodeValid) {
                    if (await _productService.SaveProduct(addProduct.ToProduct(), addProduct.Images)) {
                        return LocalRedirect("/admin/products");
                    }
                } else {
                    ViewData["Message"] = "Sku Or Barcode Must Unique";
                }
            }

            return View("Create", addProduct);
        }
    }
}