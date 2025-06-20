using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Repository.Models;
using Repository.Models.DTOs.Request;
using Repository.Models.DTOs.Response;
using Services.IService;
using System.Security.Claims;

namespace FUNewsManagement_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsArticleController : ControllerBase
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ISystemAccountService _systemAccountService;
        public NewsArticleController(INewsArticleService newsArticleService, ISystemAccountService systemAccountService)
        {
            _newsArticleService = newsArticleService;
            _systemAccountService = systemAccountService;

        }
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<TResponse<NewsArticleResponse>>> GetAll()
        {
            var news = await _newsArticleService.GetQueryable();
            var res = new TResponse<List<NewsArticleResponse>>("get news list success", news);
            return Ok(res);
        }

		[HttpGet("active")]
		[EnableQuery]
		public async Task<ActionResult<TResponse<NewsArticleResponse>>> GetAllActive()
		{
			var news = await _newsArticleService.GetActiveQueryable();
			var res = new TResponse<List<NewsArticleResponse>>("get news list success", news);
			return Ok(res);
		}
		[Authorize(Roles = "Staff")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TResponse<NewsArticleResponse>>> GetById(string id)
        {
            var news = await _newsArticleService.GetByIdAsync(id);
            var res = new TResponse<NewsArticleResponse>("get news success", news);
            return Ok(res);
        }
        [Authorize(Roles = "Staff")]
        [HttpPost]
        public async Task<ActionResult<TResponse<NewsArticleResponse>>> CreateNews([FromBody] NewsArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var account = await GetCurrentAccount();
            var news = await _newsArticleService.AddNewsArticleAsync(account,request);
            var res = new TResponse<NewsArticleResponse>("news created", news);
            return Ok(res);
        }
        [Authorize(Roles = "Staff")]
        [HttpPut("{id}")] 
        public async Task<ActionResult<TResponse<NewsArticleResponse>>> UpdateNews(string id,[FromBody] NewsArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var account = await GetCurrentAccount();

            var news = await _newsArticleService.UpdateNewsArticleAsync(account, id,request);
            var res = new TResponse<NewsArticleResponse>("news updated", news);
            return Ok(res);
        }
        [Authorize(Roles = "Staff")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(string id)
        {
            var result = await _newsArticleService.DeleteNewsArticleAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        private async Task<SystemAccount?> GetCurrentAccount()
        {
            var username = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(username)) return null;
            return await _systemAccountService.GetAccountByUsername(username);
        }
    }
}