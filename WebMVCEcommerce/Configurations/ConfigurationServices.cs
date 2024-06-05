using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebMVCEcommerce.Services.Authentication;
using WebMVCEcommerce.Services.Category;
using WebMVCEcommerce.Services.Comment;
using WebMVCEcommerce.Services.Product;

namespace WebMVCEcommerce.Configurations
{
	public static class ConfigurationServices
	{
		public static IServiceCollection ConfigureSerivces(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();

			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/User/Login";
					options.AccessDeniedPath = "/User/AccessDenied";
				});
			
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.RegisterHttpServices();


			return services;
		}

		public static IServiceCollection RegisterHttpServices(this IServiceCollection services)
		{
			var configureClient = new Action<IServiceProvider, HttpClient>(async (provider, client) =>
			{
				var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
				var claimsPrincipal = httpContextAccessor.HttpContext?.User;

				client.BaseAddress = new Uri("https://localhost:7245");

				if (claimsPrincipal != null && claimsPrincipal.Identity is ClaimsIdentity identity)
				{
					var accessToken = identity.FindFirst("jwt")?.Value;

					if (!string.IsNullOrEmpty(accessToken))
					{
						client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
					}
				}
			});

			services.AddHttpClient<IProductApiClient, ProductApiClient>(configureClient);
			services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(configureClient);
			services.AddHttpClient<ICommentApiClient, CommentApiClient>(configureClient);
			services.AddHttpClient<IAuthenticatonApiClient, AuthenticationApiClient>(configureClient);
			return services;
		}
	}
}
