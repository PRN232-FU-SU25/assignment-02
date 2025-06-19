using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class AccountDetailsModel : PageModel
{
	private readonly IApiClient _api;
	public AccountResponse Account { get; set; }
	public AccountDetailsModel(IApiClient api) => _api = api;
	public async Task OnGetAsync(int id) => Account = await _api.GetAccountByIdAsync(id);
}