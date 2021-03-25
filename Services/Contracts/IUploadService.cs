using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Warehouse.Services.Contracts
{
    public interface IUploadService
    {
        Task<String> HandleProductImage(IFormFile file);
        Task<List<String>> HandleProductImages(ICollection<IFormFile> files);        
    }
}