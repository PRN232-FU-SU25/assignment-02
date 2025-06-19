using Client.Models.Request;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
public class NewsCreateModel : PageModel
{
	private readonly IApiClient _api;
	public NewsCreateModel(IApiClient api) => _api = api;

	[BindProperty]
	public NewsArticleRequest Input { get; set; } = new();

	public List<SelectListItem> CategoryOptions { get; set; } = new();

	public async Task<IActionResult> OnGetAsync()
	{
		await LoadCategoriesAsync();
		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			// *** Nạp lại dropdown trước khi trả về View ***
			await LoadCategoriesAsync();
			return Page();
		}

		Input.NewsStatus = true;
		await _api.CreateNewsAsync(Input);
		return RedirectToPage("Index");
	}

	private async Task LoadCategoriesAsync()
	{
		var cats = await _api.GetCategoriesAsync();
		CategoryOptions = cats
			.Select(c => new SelectListItem(c.CategoryName, c.CategoryId.ToString()))
			.ToList();
	}
}