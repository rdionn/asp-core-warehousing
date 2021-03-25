using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Warehouse.Services.Contracts;
using Warehouse.Models;

namespace Warehouse.Services
{
    public class WarehouseAdminService : IWarehouseAdmin {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<WarehouseAdminService> _logger;

        public WarehouseAdminService(ApplicationContext appContext, ILogger<WarehouseAdminService> logger) {
            _applicationContext = appContext;
            _logger = logger;
        }

        public async Task<Warehouse.Models.Warehouse> GetManagedWarehouse(int userId) {
            try {
                var warehouse = await _applicationContext.WarehouseAdmins
                                    .Where(wa => wa.UserId == userId && wa.Status == "PUBLISH")
                                    .Include(wa => wa.Warehouse)
                                    .Select(wa => wa.Warehouse)
                                    .SingleOrDefaultAsync();
                return warehouse;
            } catch (Exception e) {
                _logger.LogError(e, "Warehouse Admin Error");
            }

            return null;
        }
    }
}