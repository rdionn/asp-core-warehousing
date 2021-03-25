using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Warehouse.Models;
using Warehouse.Ui;

namespace Warehouse.Services.Contracts {
    public interface IProductService
    {
        Task<PagedList<Product>> GetProducts(ProductFilter filter, int page, int entry);
        Task<PagedList<Product>> GetPublishedProducts(ProductFilter filter, int page, int entry);
        Task<Product> GetProduct(int productId);
        Task<bool> CreateProduct(String name, String sku, String barcode, int basePrice);
        Task<bool> SaveProduct(Product product, List<IFormFile> files);
        Task<bool> CheckProduct(String sku, String barcode);

        Task<List<Product>> FindProducts(String productName);
    }
}