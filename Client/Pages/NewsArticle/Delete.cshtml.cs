using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class NewsDeleteModel : PageModel
{
	private readonly IApiClient _api;
	public NewsArticleResponse News { get; set; }
	public NewsDeleteModel(IApiClient api) => _api = api;
	public async Task OnGetAsync(string id) => News = await _api.GetNewsByIdAsync(id);
	public async Task<IActionResult> OnPostAsync(string id) { await _api.DeleteNewsAsync(id); return RedirectToPage("Index"); }
}