using Client.Models.Request;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
public class AccountCreateModel : PageModel
{
	private readonly IApiClient _api;
	public AccountCreateModel(IApiClient api) => _api = api;

	[BindProperty]
	public AccountRequest Input { get; set; } = new();

	// Dữ liệu cho dropdown role
	public List<SelectListItem> RoleOptions { get; set; } = new();

	public void OnGet()
	{
		LoadRoles();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			LoadRoles();
			return Page();
		}

		await _api.CreateAccountAsync(Input);
		return RedirectToPage("Index");
	}

	private void LoadRoles()
	{
		// Giả sử bạn có 3 role: 1=Admin, 2=Staff, 3=Lecturer
		RoleOptions = new List<SelectListItem>
			{
				new SelectListItem("Staff",     "1"),
				new SelectListItem("Lecturer",  "2")
			};
	}
}