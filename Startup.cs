using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Warehouse.Models;
using Warehouse.Services.Contracts;
using Warehouse.Services;
using Warehouse.Util;
using Warehouse.Filters;

namespace Warehouse
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var mysqlConnection = Configuration.GetConnectionString("mysqlConnection");
            services.AddDbContextPool<ApplicationContext>(options => {
                options.UseMySql(mysqlConnection).UseLoggerFactory(LoggerFactory.Create(logging => logging.AddConsole()));
            });

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IWarehouseAdmin, WarehouseAdminService>();
            services.AddScoped<IShippingBill, ShippingBillService>();
            services.AddScoped<RajaOngkirService>(collection => {
                return new RajaOngkirService("691bfb645e844edd9210ad0a0b6fdb31");
            });

            services.AddScoped<WarehouseContext, WarehouseContext>();

            services.AddIdentity<User, Role>();
            services.AddScoped<IUserStore<User>, UserService>();
            services.AddScoped<IRoleStore<Role>, RoleService>(); 
            services.AddScoped<IUserRoleStore<User>, UserService>();
            services.AddScoped<IPasswordHasher<User>, BcryptHasher>();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "auth";
                options.AccessDeniedPath = "/error/access-denied";
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
