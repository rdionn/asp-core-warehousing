using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;
using Warehouse.Services.Contracts;
using Warehouse.Ui;

namespace Warehouse.Services {
    public class ProductService : IProductService {
        private readonly ApplicationContext _applicationContext;
        private readonly IUploadService _uploadService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationContext applicationContext, IUploadService uploadService, ILogger<ProductService> logger) {
            _applicationContext = applicationContext;
            _uploadService = uploadService;
            _logger = logger;
        }

        public async Task<bool> CheckProduct(String sku, String barcode) {
            try {
                var product = await _applicationContext.Products.Where(p => p.Barcode == barcode | p.Sku == sku).SingleOrDefaultAsync();

                if (product != null) {
                    return false;
                }

                return true;
            } catch (Exception e) {
                _logger.LogError(e, "Check Product Failed");
            }

            return false;
        }

        public async Task<bool> CreateProduct(String name, String sku, String barcode, int basePrice) {
            var product = new Product() {
                Sku = sku,
                Name = name,
                Barcode = barcode,
                BasePrice = basePrice
            };

            try {
                _applicationContext.Products.Add(product);
                await _applicationContext.SaveChangesAsync();
                return true;
            } catch (Exception e) {
                _logger.LogError(e, "Product Service Insert Failed");
            }

            return false;
        }

        public async Task<bool> SaveProduct(Product p, List<IFormFile> files) {
            try {
                if (files != null) {
                    List<String> uploadFiles = await _uploadService.HandleProductImages(files);
                    p.Images = new List<ProductImage>();

                    foreach(var file in uploadFiles) {
                        p.Images.Add(new ProductImage() {
                            Filename = file
                        });
                    }
                }

                _applicationContext.Products.Add(p);
                await _applicationContext.SaveChangesAsync();
                return true;
            } catch (Exception e) {
                _logger.LogError(e, "Create Product Failed");
            }

            return false;
        }

        public async Task<PagedList<Product>> GetProducts(ProductFilter filter, int page, int entry) {
            try {
                var products = _applicationContext.Products.AsQueryable();

                if (filter.Status != null) {
                    products = products.Where(p => p.Status == filter.Status);
                } else {
                    products = products.Where(p => p.Status == "PUBLISH");
                }

                if (filter.Name != null) {
                    products = products.Where(p => p.Name.Contains(filter.Name));
                }

                if (filter.Price != null) {
                    products = products.Where(p => p.BasePrice <= filter.Price);
                }

                var productResult = await products.Skip((page - 1) * entry).Take(entry).ToListAsync();
                var totalProductQuery = await products.CountAsync();

                return new PagedList<Product>() {
                    Data = productResult,
                    TotalData = totalProductQuery,
                    TotalPages = (int)Math.Round((double)totalProductQuery / entry),
                    CurrentPage = page
                };
            } catch (Exception e) {
                _logger.LogError(e, "Get Products Failed");
            }

            return new PagedList<Product>() {
                TotalPages = 0,
                TotalData = 0,
                CurrentPage = page
            };
        }

        public async Task<PagedList<Product>> GetPublishedProducts(ProductFilter filter, int page, int entry) {
            try {
                var products = _applicationContext.Products.Where(p => p.Status == "PUBLISH");

                if (filter.Name != null) {
                    products = products.Where(p => p.Name.Contains(filter.Name));
                }

                if (filter.Price != null) {
                    products = products.Where(p => p.BasePrice <= filter.Price);
                }

                var productResult = await products.Skip((page - 1) * entry).Take(entry).ToListAsync();
                var totalProductQuery = await products.CountAsync();

                return new PagedList<Product>() {
                    Data = productResult,
                    TotalData = totalProductQuery,
                    TotalPages = (int)Math.Round((double)totalProductQuery / entry),
                    CurrentPage = page
                };
            } catch (Exception e) {
                _logger.LogError(e, "Get Products Failed");
            }

            return new PagedList<Product>() {
                TotalPages = 0,
                TotalData = 0,
                CurrentPage = page
            };
        }

        public async Task<Product> GetProduct(int productId) {
            try {
                return await _applicationContext.Products.Where(p => p.Id == productId).Include(p => p.Images).SingleOrDefaultAsync();
            } catch (Exception e) {
                _logger.LogError(e, "Get Product Failed");
            }

            return null;
        }

        public async Task<List<Product>> FindProducts(string productName)
        {
            return await _applicationContext.Products.Where(p => p.Name.Contains(productName)).ToListAsync();
        }
    }
}