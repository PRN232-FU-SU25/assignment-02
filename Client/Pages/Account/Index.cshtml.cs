using Client.Models.Request;
using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
public class AccountIndexModel : PageModel
{
	private readonly IApiClient _api;
	public AccountIndexModel(IApiClient api) => _api = api;

	public List<AccountResponse> Accounts { get; set; } = new();

	[BindProperty(SupportsGet = true)]
	public int? EditId { get; set; }

	[BindProperty(SupportsGet = true)]
	public int? DeleteId { get; set; }

	[BindProperty]
	public AccountRequest Input { get; set; } = new();

	public List<SelectListItem> RoleOptions { get; set; } = new();

	public bool ShowEditModal => EditId.HasValue;
	public bool ShowDeleteModal => DeleteId.HasValue;

	public async Task OnGetAsync()
	{
		// 1. Load danh sách tài khoản & dropdown roles
		Accounts = await _api.GetAccountsAsync();
		LoadRoles();

		// 2. Nếu có editId → load dữ liệu cho form edit
		if (ShowEditModal)
		{
			var acc = Accounts.FirstOrDefault(x => x.AccountId == EditId)
					  ?? await _api.GetAccountByIdAsync(EditId.Value);

			Input = new AccountRequest
			{
				AccountName = acc.AccountName,
				AccountEmail = acc.AccountEmail,
				AccountRole = acc.AccountRole,
				// Password không cần load
			};
		}
	}

	public async Task<IActionResult> OnPostEditAsync(int editId)
	{
		// reload roles cho dropdown
		LoadRoles();

		if (!ModelState.IsValid)
		{
			EditId = editId;
			await OnGetAsync();
			return Page();
		}

		await _api.UpdateAccountAsync(editId, Input);
		return RedirectToPage(); // về Index, bỏ query-string
	}

	public async Task<IActionResult> OnPostDeleteAsync(int deleteId)
	{
		await _api.DeleteAccountAsync(deleteId);
		return RedirectToPage();
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