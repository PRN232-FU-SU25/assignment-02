using Client.Models.Request;
using Client.Models.Response;
using System.Net.Http.Headers;

namespace Client.Services
{
	public interface IApiClient
	{
		Task<AuthResponse?> LoginAsync(string username, string password);
		Task<List<AccountResponse>> GetAccountsAsync();
		Task<TResponse<AccountResponse>> CreateAccountAsync(AccountRequest req);
		Task UpdateAccountAsync(int id, AccountRequest req);
		Task DeleteAccountAsync(int id);
		Task<AccountResponse> GetAccountByIdAsync(int id);
		Task<List<CategoryResponse>> GetCategoriesAsync();
		Task<TResponse<CategoryResponse>> CreateCategoryAsync(CategoryRequest req);
		Task<TResponse<CategoryResponse>> UpdateCategoryAsync(int id, CategoryRequest req);
		Task DeleteCategoryAsync(int id);
		Task<CategoryResponse> GetCategoryByIdAsync(int id);
		Task<List<NewsArticleResponse>> GetNewsAsync();
		Task<List<NewsArticleResponse>> GetActiveNewsAsync();
		Task<TResponse<NewsArticleResponse>> CreateNewsAsync(NewsArticleRequest req);
		Task<TResponse<NewsArticleResponse>> UpdateNewsAsync(string id, NewsArticleRequest req);
		Task DeleteNewsAsync(string id);
		Task<NewsArticleResponse> GetNewsByIdAsync(string id);
	}
	public class ApiClient : IApiClient
	{
		private readonly HttpClient _http;
		private readonly IHttpContextAccessor _ctx;

		public ApiClient(HttpClient http, IHttpContextAccessor ctx)
		{
			_http = http; _ctx = ctx;
		}

		private void AttachToken()
		{
			if (_ctx.HttpContext.Request.Cookies.TryGetValue("jwt", out var token))
				_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}

		public async Task<AuthResponse?> LoginAsync(string username, string password)
		{
			var res = await _http.PostAsJsonAsync("Auth", new { username, password });
			return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<AuthResponse>() : null;
		}

		// SystemAccount
		public async Task<List<AccountResponse>> GetAccountsAsync() { AttachToken(); var r = await _http.GetFromJsonAsync<TResponse<List<AccountResponse>>>("SystemAccount"); return r!.Data; }
		public async Task<TResponse<AccountResponse>> CreateAccountAsync(AccountRequest req) { AttachToken(); var r = await _http.PostAsJsonAsync("SystemAccount", req); return (await r.Content.ReadFromJsonAsync<TResponse<AccountResponse>>())!; }
		public async Task UpdateAccountAsync(int id, AccountRequest req)
		{
			AttachToken();
			var response = await _http.PutAsJsonAsync($"SystemAccount/{id}", req);

			if (!response.IsSuccessStatusCode)
			{
				var body = await response.Content.ReadAsStringAsync();
				throw new Exception($"Update failed: {(int)response.StatusCode} {response.StatusCode}\n{body}");
			}

		}

		public async Task DeleteAccountAsync(int id) { AttachToken(); var r = await _http.DeleteAsync($"SystemAccount/{id}"); r.EnsureSuccessStatusCode(); }
		public async Task<AccountResponse> GetAccountByIdAsync(int id) { AttachToken(); var r = await _http.GetFromJsonAsync<TResponse<AccountResponse>>($"SystemAccount/{id}"); return r!.Data; }

		// Category
		public async Task<List<CategoryResponse>> GetCategoriesAsync() { AttachToken(); var r = await _http.GetFromJsonAsync<TResponse<List<CategoryResponse>>>("Category"); return r!.Data; }
		public async Task<TResponse<CategoryResponse>> CreateCategoryAsync(CategoryRequest req) { AttachToken(); var r = await _http.PostAsJsonAsync("Category", req); return (await r.Content.ReadFromJsonAsync<TResponse<CategoryResponse>>())!; }
		public async Task<TResponse<CategoryResponse>> UpdateCategoryAsync(int id, CategoryRequest req) { AttachToken(); var r = await _http.PutAsJsonAsync($"Category/{id}", req); return (await r.Content.ReadFromJsonAsync<TResponse<CategoryResponse>>())!; }
		public async Task DeleteCategoryAsync(int id) { AttachToken(); var r = await _http.DeleteAsync($"Category/{id}"); r.EnsureSuccessStatusCode(); }
		public async Task<CategoryResponse> GetCategoryByIdAsync(int id) { AttachToken(); var r = await _http.GetFromJsonAsync<TResponse<CategoryResponse>>($"Category/{id}"); return r!.Data; }

		// NewsArticle
		public async Task<List<NewsArticleResponse>> GetNewsAsync()
		{
			AttachToken();
			var r = await _http.GetFromJsonAsync<TResponse<List<NewsArticleResponse>>>("NewsArticle");
			return r!.Data;
		}

		public async Task<List<NewsArticleResponse>> GetActiveNewsAsync()
		{
			AttachToken();
			var r = await _http.GetFromJsonAsync<TResponse<List<NewsArticleResponse>>>("NewsArticle/active");
			return r!.Data;
		}
		public async Task<TResponse<NewsArticleResponse>> CreateNewsAsync(NewsArticleRequest req) { AttachToken(); var r = await _http.PostAsJsonAsync("NewsArticle", req); return (await r.Content.ReadFromJsonAsync<TResponse<NewsArticleResponse>>())!; }
		public async Task<TResponse<NewsArticleResponse>> UpdateNewsAsync(string id, NewsArticleRequest req) { AttachToken(); var r = await _http.PutAsJsonAsync($"NewsArticle/{id}", req); return (await r.Content.ReadFromJsonAsync<TResponse<NewsArticleResponse>>())!; }
		public async Task DeleteNewsAsync(string id)
		{
			AttachToken();
			var response = await _http.DeleteAsync($"NewsArticle/{id}");
			response.EnsureSuccessStatusCode();
		}
		public async Task<NewsArticleResponse> GetNewsByIdAsync(string id) { AttachToken(); var r = await _http.GetFromJsonAsync<TResponse<NewsArticleResponse>>($"NewsArticle/{id}"); return r!.Data; }
	}
}
