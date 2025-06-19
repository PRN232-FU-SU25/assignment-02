using Client.Models.Response;
using Client.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class ProfileModel : PageModel
{
	private readonly IApiClient _api;
	public AccountResponse Me { get; set; } = new();
	public ProfileModel(IApiClient api) => _api = api;
	public async Task OnGetAsync()
	{
		// assume current user ID passed in token
		var cookie = HttpContext.Request.Cookies["jwt"];
		var handler = new JwtSecurityTokenHandler();
		var jwt = handler.ReadJwtToken(cookie);
		var idClaim = jwt.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
		Me = await _api.GetAccountByIdAsync(short.Parse(idClaim.Value));
	}
}