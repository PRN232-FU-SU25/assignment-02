using Client.Models.Request;
using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class NewsIndexModel : PageModel
{
	private readonly IApiClient _api;
	public NewsIndexModel(IApiClient api) => _api = api;

	public List<NewsArticleResponse> NewsList { get; set; } = new();
	public List<SelectListItem> CategoryOptions { get; set; } = new();

	[BindProperty(SupportsGet = true)]
	public string? EditId { get; set; }

	[BindProperty(SupportsGet = true)]
	public string? DeleteId { get; set; }

	[BindProperty]
	public NewsArticleRequest Input { get; set; } = new();

	public bool ShowEditModal => !string.IsNullOrEmpty(EditId);
	public bool ShowDeleteModal => !string.IsNullOrEmpty(DeleteId);

	public async Task OnGetAsync()
	{
		await LoadNewsAsync();
		await LoadCategoriesAsync();

		if (ShowEditModal)
		{
			var n = NewsList.FirstOrDefault(x => x.NewsArticleId == EditId)
					?? await _api.GetNewsByIdAsync(EditId!);
			Input = new NewsArticleRequest
			{
				NewsTitle = n.NewsTitle,
				Headline = n.Headline,
				NewsContent = n.NewsContent,
				NewsSource = n.NewsSource,
				CategoryId = n.CategoryId,
				NewsStatus = n.NewsStatus == true
			};
		}
	}

	public async Task<IActionResult> OnPostEditAsync(string editId)
	{
		if (!ModelState.IsValid)
		{
			EditId = editId;
			await OnGetAsync();
			return Page();
		}
		await _api.UpdateNewsAsync(editId, Input);
		return RedirectToPage();
	}

	public async Task<IActionResult> OnPostDeleteAsync(string deleteId)
	{
		await _api.DeleteNewsAsync(deleteId);
		return RedirectToPage();
	}

	private async Task LoadNewsAsync()
	{
		NewsList = await _api.GetNewsAsync();
	}

	private async Task LoadCategoriesAsync()
	{
		var cats = await _api.GetCategoriesAsync();
		CategoryOptions = cats
			.Select(c => new SelectListItem(c.CategoryName, c.CategoryId.ToString()))
			.ToList();
	}
}