using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Middleware;
using NewsApp.Repositories.AdminPanelRepos;
using NewsApp.Repositories.NewsArticles;
using NewsApp.Repositories.Users;
using NewsApp.Services.AdminPanelServices;
using NewsApp.Services.NewsArticles;
using NewsApp.Services.Users;
using System.Globalization;

namespace NewsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NewsAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NewsAppContext") ?? 
                throw new InvalidOperationException("Connection string 'NewsAppContext' not found.")));

            builder.Services.AddIdentityCore<User>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
                options.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequiredLength = 8,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false,
                };
            })
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<NewsAppContext>();

            builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new CultureInfo[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<INewsArticlesRepository, NewsArticlesRepository>();
            builder.Services.AddScoped<INewsArticlesService, NewsArticlesService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IAdminPanelRepository, AdminPanelRepository>();
            builder.Services.AddScoped<IAdminPanelService, AdminPanelService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(); ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>?>();
            if (locOptions != null)
            {
                app.UseRequestLocalization(locOptions.Value);
            }

            app.UseCultureMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{culture}/{controller=Home}/{action=Index}/{id?}",
                defaults: new { culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName });


            app.Run();
        }
    }
}




/*
            -----------\ 
           /            \
          /              \
         |‾‾‾‾\  _‾‾‾    |
         \  👁\    👁   |
          \    |        /
           \           /
            \    *    /
             \ ****  / 
              \    /              
               |‾‾‾|
 


 

 
 
 
 
 
 
 
 
 */