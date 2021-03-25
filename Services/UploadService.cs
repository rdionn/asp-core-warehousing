using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Warehouse.Services.Contracts;

namespace Warehouse.Services
{
    public class UploadService : IUploadService {
        private readonly IWebHostEnvironment _hostEnv;
        private readonly ILogger<UploadService> _logger;
        private String productPath;

        public UploadService(IWebHostEnvironment hostEnv, ILogger<UploadService> logger) {
            _hostEnv = hostEnv;
            _logger = logger;

            productPath = Path.Combine(_hostEnv.WebRootPath, "uploads", "products");

            if (!Directory.Exists(productPath)) {
                Directory.CreateDirectory(productPath);
            }
        }

        public async Task<String> HandleProductImage(IFormFile file) {
            try {
                var fileTarget = Path.Combine(productPath, file.FileName);

                using (var fileStream = new FileStream(fileTarget, FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }

            } catch (Exception e) {
                _logger.LogError(e, "Upload Service Failed");
                return null;
            }

            return file.FileName;
        }

        public async Task<List<String>> HandleProductImages(ICollection<IFormFile> files) {
            var list = new List<String>();
            foreach (var file in files) {
                list.Add(await HandleProductImage(file));
            }

            return list.Where(x => x != null).ToList();
        }
    }
}