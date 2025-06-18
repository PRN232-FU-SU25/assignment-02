using Repository.Models.DTOs.Request;
using Repository.Models.DTOs.Response;
using Repository.Models.DTOs.Resquest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetQueryable();
        Task<CategoryResponse?> GetByIdAsync(short id);
        Task<CategoryResponse> CreateAsync(CategoryRequest dto);
        Task<CategoryResponse> UpdateAsync(short id, CategoryRequest dto);
        Task<(bool Success, string Message)> DeleteAsync(short id);
    }
}
