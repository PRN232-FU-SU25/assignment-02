using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
	private readonly IApiClient _api;
	public List<NewsArticleResponse> NewsList { get; set; } = new();
	public IndexModel(IApiClient api) => _api = api;
	public async Task OnGetAsync() => NewsList = await _api.GetActiveNewsAsync();
}