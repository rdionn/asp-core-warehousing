using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Warehouse.Services.Contracts;
using Warehouse.Models;
using Warehouse.Ui;

namespace Warehouse.Services
{
    public class ShippingBillService : IShippingBill {
        private readonly ApplicationContext _appContext;
        private readonly ILogger<ShippingBillService> _logger;

        public ShippingBillService(ApplicationContext appContext, ILogger<ShippingBillService> logger) {
            _appContext = appContext;
            _logger = logger;
        }

        public async Task<int> CreateShippingBill(int warehouseId) {
            try {
                var bill = new ShippingBill() {
                    WarehouseId = warehouseId,
                    BillInvoice = String.Format("{0}{1}", new object[] {  warehouseId, DateTime.Now.ToString("yyMMddhhmmss")} ),
                    Status = "DRAFT"
                };

                _appContext.ShippingBills.Add(bill);
                await _appContext.SaveChangesAsync();

                return bill.Id;
            } catch (Exception e) {
                _logger.LogError(e, "Shipping Service Error");
            }

            return -1;
        }

        public async Task<bool> AddShippingProduct(int shippingBillId, ShippingAddBillProduct addProduct) {
            try {
                var bill = await _appContext.ShippingBills.Where(b => b.Id == shippingBillId).SingleOrDefaultAsync();

                if (bill != null) {
                    var shippingProduct = addProduct.ToShippingBillProduct();

                    _appContext.ShippingBillProducts.Add(shippingProduct);
                    await _appContext.SaveChangesAsync();

                    return true;
                }
            } catch (Exception e) {
                _logger.LogError(e, "Error Add Shipping Product");
            }

            return false;
        }

        public async Task<PagedList<ShippingBill>> GetShippingBill(int warehouseId, int page, int entry) {
            try {
                var billQuery = _appContext.ShippingBills.Where(sb => sb.WarehouseId == warehouseId).AsQueryable();
                var bills = await billQuery.Skip((page - 1) & entry).Take(entry).ToListAsync();
                var totalBills = await billQuery.CountAsync();

                return new PagedList<ShippingBill>() {
                    Data = bills,
                    TotalPages = (int)Math.Round((double)totalBills / entry),
                    CurrentPage = page,
                    TotalData = totalBills
                };
            } catch (Exception e) {
                _logger.LogError(e, "Shipping Bill Error");
            }

            return new PagedList<ShippingBill>();
        }

        public async Task<PagedList<ShippingBillProduct>> GetShippingProducts(int shippingId, int page, int entry) {
            try {
                var productQuery = _appContext.ShippingBillProducts.Where(sp => sp.ShippingBillId == shippingId).AsQueryable();

                var products = await productQuery.Include(p => p.Product).Skip((page - 1) * entry).Take(entry).ToListAsync();
                var totalProducts = await productQuery.CountAsync();

                return new PagedList<ShippingBillProduct>() {
                    Data = products,
                    TotalPages = (int)Math.Round((double)totalProducts / entry),
                    TotalData = totalProducts,
                    CurrentPage = page
                };
            } catch (Exception e) {
                _logger.LogError(e, "Get Shipping Product Bill Error");
            }

            return new PagedList<ShippingBillProduct>();
        }

        public async Task<ShippingBill> GetShippingBill(int shippingBillId)
        {
            try
            {
                return await _appContext.ShippingBills.Where(sb => sb.Id == shippingBillId)
                    .Include(sb => sb.Warehouse)
                    .SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Get Shipping Bill");
            }

            return null;
        }

        public async Task<PagedList<ShippingBill>> GetShippingBillWarehouse(int warehouseId, int page, int entry)
        {
            try
            {
                var billQuery = _appContext.ShippingBills
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(sb => sb.WarehouseId == warehouseId && sb.Status == "SENDING");

                var bills = await billQuery.Skip((page - 1) * entry).Take(entry).ToListAsync();
                var countData = await billQuery.CountAsync();
                
                return new PagedList<ShippingBill>()
                {
                    Data = bills,
                    TotalData = countData,
                    TotalPages = (int)Math.Round((double)countData / entry),
                    CurrentPage = page
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Get Shipping Bill Warehouse");
            }

            return new PagedList<ShippingBill>();
        }

        public async Task<bool> SendShipment(int shippmentId)
        {
            try
            {
                var bill = await _appContext.ShippingBills.Where(sb => sb.Id == shippmentId && sb.Status == "DRAFT")
                    .SingleOrDefaultAsync();

                if (bill != null)
                {
                    bill.Status = "SENDING";
                    await _appContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Send Shipment Bill");
            }

            return false;
        }
    }
}