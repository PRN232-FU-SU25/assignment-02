using System.Net;
using System.Net.Http.Headers;

namespace Client.Services
{
	public class AuthHandler : DelegatingHandler
	{
		private readonly IHttpContextAccessor _ctx;
		public AuthHandler(IHttpContextAccessor ctx) => _ctx = ctx;

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (_ctx.HttpContext.Request.Cookies.TryGetValue("jwt", out var token))
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await base.SendAsync(request, cancellationToken);
			if (response.StatusCode == HttpStatusCode.Unauthorized ||
				response.StatusCode == HttpStatusCode.Forbidden)
			{
				_ctx.HttpContext.Response.Cookies.Delete("jwt");
				_ctx.HttpContext.Response.Redirect("/NotPermission");
			}
			return response;
		}
	}
}
