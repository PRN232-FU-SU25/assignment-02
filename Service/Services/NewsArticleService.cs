using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Repository.Models;
using Repository.Models.DTOs.Request;
using Repository.Models.DTOs.Response;
using Repository.Repositories;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly IMapper _mapper;
        public NewsArticleService(INewsArticleRepo newsArticleRepo, IMapper mapper)
        {
            _newsArticleRepo = newsArticleRepo;
            _mapper = mapper;
        }

        public async Task<NewsArticleResponse> AddNewsArticleAsync(SystemAccount acc ,NewsArticleRequest NewsArticle)
        {
            int id = await _newsArticleRepo.CountAsync() + 1;
            var news = _mapper.Map<NewsArticle>(NewsArticle);
            news.NewsArticleId = id.ToString();
            news.CreatedById = acc.AccountId;
            await _newsArticleRepo.AddAsync(news);
            var res = _mapper.Map<NewsArticleResponse>(news);
            return res;
        }

        public async Task<(bool Success, string Message)> DeleteNewsArticleAsync(string id)
        {
            var account = await _newsArticleRepo.GetNewsArticleById(id);
            if (account == null) return  (false, "Account not found");

            await _newsArticleRepo.Delete(account);
            return (true, "Account deleted");
        }

        public async Task<NewsArticleResponse> GetByIdAsync(string id)
        {
            var news = await _newsArticleRepo.GetNewsArticleById(id);
            if(news == null)
            {
                throw new Exception("NewsArticle not found");
            }
            var res = _mapper.Map<NewsArticleResponse>(news);
            return res;
        }

        public async Task<List<NewsArticleResponse>> GetQueryable()
        {
            var accs = await _newsArticleRepo.GetQueryable();
            var list = await accs.ToListAsync();
            var res = _mapper.Map<List<NewsArticleResponse>>(list);
            return res;
        }

		public async Task<List<NewsArticleResponse>> GetActiveQueryable()
		{
			var accs = await _newsArticleRepo.GetActiveQueryable();
			var list = await accs.ToListAsync();
			var res = _mapper.Map<List<NewsArticleResponse>>(list);
			return res;
		}

		public async  Task<NewsArticleResponse> UpdateNewsArticleAsync(string id ,NewsArticleRequest NewsArticle)
        {
            var news = await _newsArticleRepo.GetNewsArticleById(id);
            if (news == null)
            {
                throw new Exception("NewsArticle not found");
            }

            news.NewsTitle = NewsArticle.NewsTitle;
            news.Headline = NewsArticle.Headline;
            news.NewsContent = NewsArticle.NewsContent;
            news.NewsSource = NewsArticle.NewsSource;
            news.CategoryId = NewsArticle.CategoryId;
            news.NewsStatus = NewsArticle.NewsStatus;

			await _newsArticleRepo.UpdateAsync(news);
            var res =  _mapper.Map<NewsArticleResponse>(news);
            return res;
        }
    }
}