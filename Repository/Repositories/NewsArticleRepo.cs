using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class NewsArticleRepo : Repository<NewsArticle>, INewsArticleRepo
    {
        private readonly FUNewsManagementDbContext _context;
        public NewsArticleRepo(FUNewsManagementDbContext context) : base(context)
        {
            _context = context;
        }
        public async  Task Delete(NewsArticle acc)
        {
            _context.NewsArticles.Remove(acc);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.NewsArticles.CountAsync();
        }

        public async Task<NewsArticle> GetNewsArticleById(string Id)
        {
            return await _context.NewsArticles.
                Include(c => c.Category)
               .FirstOrDefaultAsync(s => s.NewsArticleId == Id);
        }

        public async Task<IQueryable<NewsArticle>> GetQueryable()
        {
            return  _context.NewsArticles.
                Include(c => c.Category)
                .ThenInclude(p => p.ParentCategory).
                Include(a => a.CreatedBy)
                .AsQueryable();
        }

		public async Task<IQueryable<NewsArticle>> GetActiveQueryable()
		{
			return _context.NewsArticles.
				Include(c => c.Category)
				.ThenInclude(p => p.ParentCategory).
				Include(a => a.CreatedBy)
				.AsQueryable().Where(x => x.NewsStatus == true);
		}
	}
}
