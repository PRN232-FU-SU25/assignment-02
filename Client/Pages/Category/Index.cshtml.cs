using Client.Models.Request;
using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
public class CategoryIndexModel : PageModel
{
	private readonly IApiClient _api;
	public CategoryIndexModel(IApiClient api) => _api = api;

	// Danh sách hiện có
	public List<CategoryResponse> Categories { get; set; } = new();

	// Dùng để bind form edit
	[BindProperty(SupportsGet = true)]
	public int? EditId { get; set; }

	[BindProperty(SupportsGet = true)]
	public int? DeleteId { get; set; }

	[BindProperty]
	public CategoryRequest Input { get; set; } = new();

	// Dropdown parent
	public List<SelectListItem> ParentOptions { get; set; } = new();

	public bool ShowEditModal => EditId.HasValue;
	public bool ShowDeleteModal => DeleteId.HasValue;

	public async Task OnGetAsync()
	{
		// load data
		Categories = await _api.GetCategoriesAsync();

		// dropdown parent (cho edit)
		ParentOptions = Categories
			.Select(c => new SelectListItem(c.CategoryName, c.CategoryId.ToString()))
			.Prepend(new SelectListItem("-- Không chọn --", ""))
			.ToList();

		if (ShowEditModal)
		{
			var c = Categories.FirstOrDefault(x => x.CategoryId == EditId)
					?? await _api.GetCategoryByIdAsync(EditId.Value);

			Input = new CategoryRequest
			{
				CategoryName = c.CategoryName,
				CategoryDesciption = c.CategoryDesciption,
				ParentCategoryId = c.ParentCategoryId,
				IsActive = c.IsActive == true
			};
		}
	}

	public async Task<IActionResult> OnPostEditAsync(int editId)
	{
		if (!ModelState.IsValid)
		{
			EditId = editId;
			await OnGetAsync();
			return Page();
		}

		await _api.UpdateCategoryAsync(editId, Input);
		return RedirectToPage();
	}

	public async Task<IActionResult> OnPostDeleteAsync(int deleteId)
	{
		await _api.DeleteCategoryAsync(deleteId);
		return RedirectToPage();
	}
}