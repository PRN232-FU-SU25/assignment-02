using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class AccountDeleteModel : PageModel
{
	private readonly IApiClient _api;
	public AccountResponse Account { get; set; }
	public AccountDeleteModel(IApiClient api) => _api = api;
	public async Task OnGetAsync(int id) => Account = await _api.GetAccountByIdAsync(id);
	public async Task<IActionResult> OnPostAsync(int id)
	{
		await _api.DeleteAccountAsync(id);
		return RedirectToPage("Index");
	}
}