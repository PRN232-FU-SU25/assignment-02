using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class NewsDetailsModel : PageModel
{
	private readonly IApiClient _api;
	public NewsArticleResponse News { get; set; }
	public NewsDetailsModel(IApiClient api) => _api = api;
	public async Task OnGetAsync(string id) => News = await _api.GetNewsByIdAsync(id);
}