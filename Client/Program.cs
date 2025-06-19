using Client.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Client
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddHttpContextAccessor();

			// Register AuthHandler for automatic JWT attach & redirect
			builder.Services.AddTransient<AuthHandler>();

			// Configure HttpClient with AuthHandler
			builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
			})
			.AddHttpMessageHandler<AuthHandler>();

			// Register AuthService
			builder.Services.AddScoped<IAuthService, AuthService>();

			// Cookie auth (for Razor Pages)
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			  .AddCookie(options =>
			  {
				  options.Cookie.Name = "AuthCookie";      // <— tên riêng cho cookie-auth
				  options.LoginPath = "/Login";
				  options.AccessDeniedPath = "/NotPermission";
				  options.Events.OnRedirectToLogin = ctx =>
				  {
					  if (ctx.Request.Path.StartsWithSegments("/api"))
					  {
						  ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
						  return Task.CompletedTask;
					  }
					  ctx.Response.Redirect(ctx.RedirectUri);
					  return Task.CompletedTask;
				  };
			  });

			builder.Services.AddAuthorization(options =>
			{
				// Chỉ Admin mới được /Account
				options.AddPolicy("RequireAdmin", policy =>
					policy.RequireRole("Admin"));

				// Admin hoặc Staff mới được /Category và /NewsArticle
				options.AddPolicy("RequireStaff", policy =>
					policy.RequireRole("Staff"));
			});

			// Razor Pages with folder auth
			builder.Services.AddRazorPages(options =>
			{
				options.Conventions.AuthorizeFolder("/Account", "RequireAdmin");
				options.Conventions.AuthorizeFolder("/Category", "RequireStaff");
				options.Conventions.AuthorizeFolder("/NewsArticle", "RequireStaff");
				options.Conventions.AuthorizeFolder("/Profile");
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.MapRazorPages();

			app.Run();
		}
	}
}
