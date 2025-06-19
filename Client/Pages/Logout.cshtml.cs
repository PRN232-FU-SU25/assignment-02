using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
public class LogoutModel : PageModel
{
	private readonly IAuthService _auth;
	public LogoutModel(IAuthService auth) => _auth = auth;
	public async Task OnGetAsync() { await _auth.LogoutAsync(); Response.Redirect("/Login"); }
}