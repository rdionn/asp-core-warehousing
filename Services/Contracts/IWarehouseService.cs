using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.Services.Contracts {
    public interface IWarehouseService
    {
        Task<PagedList<Warehouse.Models.Warehouse>> GetWarehouses(int page, int entry);
        Task<PagedList<ProductWarehouse>> GetProducts(int warehouseId, int page, int entry);
        Task<PagedList<ProductWarehouse>> GetAvailableProducts(int warehouseId, int page, int entry);
        Task<PagedList<ProductWarehouse>> GetProductLocations(int productId, int page, int entry);
        Task<bool> AddWarehouse(Warehouse.Models.Warehouse warehouse);

        Task<List<Warehouse.Models.Warehouse>> GetAllWarehouses();
    }
}