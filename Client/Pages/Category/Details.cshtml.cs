using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class CategoryDetailsModel : PageModel
{
	private readonly IApiClient _api;
	public CategoryResponse Category { get; set; }
	public CategoryDetailsModel(IApiClient api) => _api = api;
	public async Task OnGetAsync(int id) => Category = await _api.GetCategoryByIdAsync(id);
}