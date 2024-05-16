using NewsApp.Shared;
using System.Globalization;

namespace NewsApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public CultureMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public Task Invoke(HttpContext context)
        {
            var culture = context.Request.Path.Value?.Split('/')[1];

            if (!string.IsNullOrEmpty(culture))
            {
                var supportedCultures = _configuration.GetSection(AppSettingsKeys.AppSettings_SupportedCultures)
                    .Get<string[]>();

                if (supportedCultures == null) return _next(context);

                if (supportedCultures.Contains(culture))
                {
                    CultureInfo.CurrentCulture = new CultureInfo(culture);
                    CultureInfo.CurrentUICulture = new CultureInfo(culture);
                }
            }

            return _next(context);
        }
    }

    public static class CultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseCultureMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultureMiddleware>();
        }
    }
}
