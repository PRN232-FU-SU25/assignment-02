namespace Client.Services
{
	public interface IAuthService { 
		Task<string?> LoginAsync(string user, string pass); 
		Task LogoutAsync(); 
	}
	public class AuthService : IAuthService
	{
		private readonly IApiClient _api;
		private readonly IHttpContextAccessor _ctx;
		public AuthService(IApiClient api, IHttpContextAccessor ctx) { _api = api; _ctx = ctx; }
		public async Task<string?> LoginAsync(string user, string pass)
		{
			var auth = await _api.LoginAsync(user, pass);
			return auth?.AccessToken;
		}
		public Task LogoutAsync() { _ctx.HttpContext.Response.Cookies.Delete("jwt"); return Task.CompletedTask; }
	}
}
