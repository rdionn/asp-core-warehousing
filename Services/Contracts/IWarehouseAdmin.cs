using System;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.Services.Contracts
{
    public interface IWarehouseAdmin
    {
        Task<Warehouse.Models.Warehouse> GetManagedWarehouse(int userId);
    }
}