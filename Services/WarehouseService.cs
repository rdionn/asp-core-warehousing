using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;
using Warehouse.Services.Contracts;

namespace Warehouse.Services {
    public class WarehouseService : IWarehouseService {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<WarehouseService> _logger;
        
        public WarehouseService(ApplicationContext applicationContext, ILogger<WarehouseService> logger) {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public async Task<PagedList<ProductWarehouse>> GetProducts(int warehouseId, int page, int entry) {
            try {
                var productQuery = _applicationContext
                            .ProductWarehouse
                            .Where(pw => pw.WarehouseId == warehouseId)
                            .AsQueryable();

                var products = await productQuery
                                .Skip((page - 1) * entry)
                                .Take(entry)
                                .ToListAsync();

                var totalProducts = await productQuery.CountAsync();

                return new PagedList<ProductWarehouse>() {
                    Data = products,
                    TotalPages = (int)Math.Round((double)totalProducts / entry),
                    CurrentPage = page,
                    TotalData = totalProducts
                };
            } catch (Exception e) {
                _logger.LogError(e, "Failed Get Products");
            }
           
           return new PagedList<ProductWarehouse>() {
               TotalPages = 0,
               CurrentPage = page,
               TotalData = 0
           };
        }

        public async Task<PagedList<Warehouse.Models.Warehouse>> GetWarehouses(int page, int entry) {
            try {
                var warehouses = await _applicationContext.Warehouses.Where(w => w.Status == "PUBLISH").Skip((page - 1) * entry).Take(entry).ToListAsync();
                var totalWarehouse = await _applicationContext.Warehouses.Where(w => w.Status == "PUBLISH").CountAsync();

                return new PagedList<Warehouse.Models.Warehouse>() {
                    Data = warehouses,
                    TotalPages = (int)Math.Round((double)totalWarehouse / entry),
                    TotalData = totalWarehouse,
                    CurrentPage = page
                };
            } catch (Exception e) {
                _logger.LogError(e, "Get Warehouse Failed");
            }

            return new PagedList<Warehouse.Models.Warehouse>();
        }

        public async Task<PagedList<ProductWarehouse>> GetAvailableProducts(int warehouseId, int page, int entry) {
            try {
                var productQuery = _applicationContext
                                .ProductWarehouse
                                .AsQueryable()
                                .Where(pw => pw.WarehouseId == warehouseId && pw.Stock > 0);

                var products = await productQuery.Skip((page - 1) * entry)
                                .Take(entry)
                                .ToListAsync();

                var totalProducts = await productQuery.CountAsync();

                return new PagedList<ProductWarehouse>() {
                    Data = products,
                    TotalPages = (int)Math.Round((double)totalProducts / entry),
                    CurrentPage = page,
                    TotalData = totalProducts
                };
            } catch (Exception e) {
                _logger.LogError(e, "Available Product Error");
            }

            return new PagedList<ProductWarehouse>();
        }

        public async Task<PagedList<ProductWarehouse>> GetProductLocations(int productId, int page, int entry) {
            try {
                var productWarehouseQuery = _applicationContext.ProductWarehouse.AsQueryable();

                var productWarehouse = await productWarehouseQuery
                                                .Where(pw => pw.ProductId == productId)
                                                .Include(pw => pw.Warehouse)
                                                .Skip((page - 1) * entry)
                                                .Take(entry)
                                                .ToListAsync();
                var totalProductWarehouse = await productWarehouseQuery.Where(pw => pw.ProductId == productId).CountAsync();

                return new PagedList<ProductWarehouse>() {
                    Data = productWarehouse,
                    TotalData = totalProductWarehouse,
                    TotalPages = (int)Math.Round((double)totalProductWarehouse / entry),
                    CurrentPage = page
                };
            } catch (Exception e) {
                _logger.LogError(e, "Failed Get Product Location");
            }

            return new PagedList<ProductWarehouse>();
        }

        public async Task<bool> AddWarehouse(Warehouse.Models.Warehouse warehouse) {
            try {
                _applicationContext.Warehouses.Add(warehouse);
                await _applicationContext.SaveChangesAsync();

                return true;
            } catch (Exception e) {
                _logger.LogError(e, "Add Warehouse Failed");
            }

            return false;
        }

        public async Task<List<Warehouse.Models.Warehouse>> GetAllWarehouses()
        {
            try
            {
                return await _applicationContext.Warehouses.Where(w => w.Status == "PUBLISH").ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get Warehouse Failed");
            }

            return new List<Warehouse.Models.Warehouse>();
        }
    }
}