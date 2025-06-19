using Client.Models.Request;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
public class AccountEditModel : PageModel
{
	private readonly IApiClient _api;
	public AccountEditModel(IApiClient api) => _api = api;

	[BindProperty]
	public AccountRequest Input { get; set; } = new();

	public int Id { get; set; }

	// Dropdown role
	public List<SelectListItem> RoleOptions { get; set; } = new();

	public async Task OnGetAsync(int id)
	{
		Id = id;
		LoadRoles();

		var acc = await _api.GetAccountByIdAsync(id);
		Input = new AccountRequest
		{
			AccountName = acc.AccountName,
			AccountEmail = acc.AccountEmail,
			AccountRole = acc.AccountRole,
			// bạn không cần password ở edit
		};
	}

	public async Task<IActionResult> OnPostAsync(int id)
	{
		Id = id;
		LoadRoles();

		if (!ModelState.IsValid)
			return Page();

		await _api.UpdateAccountAsync(id, Input);
		return RedirectToPage("Index");
	}

	private void LoadRoles()
	{
		RoleOptions = new List<SelectListItem>
			{
				new SelectListItem("Staff",    "1"),
				new SelectListItem("Lecturer", "2")
			};
	}
}