using Client.Models.Request;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
public class CategoryCreateModel : PageModel
{
	private readonly IApiClient _api;
	public CategoryCreateModel(IApiClient api) => _api = api;

	[BindProperty]
	public CategoryRequest Input { get; set; } = new();

	// Dữ liệu cho dropdown
	public List<SelectListItem> ParentCategoryOptions { get; set; } = new();

	public async Task OnGetAsync()
	{
		await LoadParentCategoriesAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			await LoadParentCategoriesAsync();
			return Page();
		}

		Input.IsActive = true;

		await _api.CreateCategoryAsync(Input);
		return RedirectToPage("Index");
	}

	private async Task LoadParentCategoriesAsync()
	{
		var cats = await _api.GetCategoriesAsync();
		ParentCategoryOptions = cats
			.Select(c => new SelectListItem(c.CategoryName, c.CategoryId.ToString()))
			.Prepend(new SelectListItem("-- Không chọn --", ""))  // tùy chọn không có parent
			.ToList();
	}
}