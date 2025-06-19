using Client.Models.Request;
using Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

public class LoginModel : PageModel
{
	private readonly IAuthService _auth;
	[BindProperty] public LoginRequest Input { get; set; } = new();
	public string? ErrorMessage { get; set; }
	public LoginModel(IAuthService auth) => _auth = auth;
	public void OnGet() { }
	public async Task<IActionResult> OnPostAsync()
	{
		var token = await _auth.LoginAsync(Input.username, Input.password);
		if (token == null)
		{
			ErrorMessage = "Sai thông tin";
			return Page();
		}

		// 1) Lưu JWT để HttpClient gọi API
		Response.Cookies.Append("jwt", token, new CookieOptions { HttpOnly = true });

		// 2) Giải mã token để lấy các claim cho cookie-auth
		var handler = new JwtSecurityTokenHandler();
		var jwtToken = handler.ReadJwtToken(token);
		var id = jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
		var email = jwtToken.Claims.First(c => c.Type == ClaimTypes.Email).Value;
		var name = jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value;
		var role = jwtToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, id),
			new Claim(ClaimTypes.Email, email),
			new Claim(ClaimTypes.Name, name),
			new Claim(ClaimTypes.Role, role)
		};
		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);

		// Issue the cookie-auth ticket
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

		// 3) Redirect theo role như trước
		if ("Admin".Equals(role)) return RedirectToPage("/Account/Index");
		if ("Staff".Equals(role)) return RedirectToPage("/Category/Index");
		if ("Lecturer".Equals(role)) return RedirectToPage("/Index");
		return RedirectToPage("/Index");
	}
}