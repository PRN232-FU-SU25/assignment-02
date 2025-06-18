using Repository.Models;
using Repository.Models.DTOs.Request;
using Repository.Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface INewsArticleService 
    {
        Task<List<NewsArticleResponse>> GetQueryable();
        Task<NewsArticleResponse> GetByIdAsync(string id);
        Task<NewsArticleResponse> AddNewsArticleAsync(SystemAccount acc ,NewsArticleRequest NewsArticle);
        Task<NewsArticleResponse> UpdateNewsArticleAsync(string id,NewsArticleRequest NewsArticle);
        Task<(bool Success, string Message)> DeleteNewsArticleAsync(string id);
    }
}
