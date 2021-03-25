using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Warehouse.Services.Contracts;
using Warehouse.Models;

namespace Warehouse.Filters {
    public class WarehouseContext : IAsyncActionFilter {
        private readonly IWarehouseAdmin _warehouseAdmin;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<WarehouseContext> _logger;

        public WarehouseContext(IWarehouseAdmin warehouseAdmin, UserManager<User> userManager, ILogger<WarehouseContext> logger) {
            _warehouseAdmin = warehouseAdmin;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var httpContext = context.HttpContext;
            var user = await _userManager.GetUserAsync(httpContext.User);

            if (user != null) {
                var warehouse = await _warehouseAdmin.GetManagedWarehouse(user.Id);

                if (warehouse != null) {
                    httpContext.Items["WarehouseContext"] = warehouse;
                    await next();
                }
            }

            context.Result = new LocalRedirectResult("/error/warehouse-not-found");
        }
    }
}