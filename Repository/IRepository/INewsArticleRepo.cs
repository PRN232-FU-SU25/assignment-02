using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface INewsArticleRepo : IRepository<NewsArticle>
    {
        Task<NewsArticle> GetNewsArticleById(string Id);
        Task<int> CountAsync();
        Task Delete(NewsArticle acc);
        Task<IQueryable<NewsArticle>> GetQueryable();
    }
}
