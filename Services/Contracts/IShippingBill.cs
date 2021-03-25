using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Warehouse.Models;
using Warehouse.Ui;

namespace Warehouse.Services.Contracts
{
    public interface IShippingBill
    {
        Task<int> CreateShippingBill(int warehouseId);
        Task<bool> AddShippingProduct(int shippingBillId, ShippingAddBillProduct addProduct);
        Task<PagedList<ShippingBill>> GetShippingBill(int warehouseId, int page, int entry);
        Task<PagedList<ShippingBillProduct>> GetShippingProducts(int shippingId, int page, int entry);
        Task<ShippingBill> GetShippingBill(int shippingBillId);
    }
}