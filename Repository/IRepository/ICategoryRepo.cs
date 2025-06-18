using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICategoryRepo : IRepository<Category>
    {
        Task<IQueryable<Category>> GetQueryable();
        Task<Category> GetCategoryById(short Id);
        Task<bool> HasNewsArticlesAsync(short categoryId);
        Task Delete(Category acc);
    }
}
