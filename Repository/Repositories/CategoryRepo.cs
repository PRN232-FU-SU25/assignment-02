using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.IRepository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        private readonly FUNewsManagementDbContext _context;
        public CategoryRepo(FUNewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(Category cate)
        {
            _context.Categories.Remove(cate);
            await _context.SaveChangesAsync();
        }
        public async Task<Category> GetCategoryById(short Id)
        {
            return await _context.Categories.
                 Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(s => s.CategoryId == Id);
        }

        public async  Task<IQueryable<Category>> GetQueryable()
        {
            return _context.Categories.
                 Include(c => c.ParentCategory).
                 AsQueryable();
        }

        public async Task<bool> HasNewsArticlesAsync(short categoryId)
        {
            return await _context.Set<NewsArticle>().AnyAsync(x => x.CategoryId == categoryId);
        }
    }
}
