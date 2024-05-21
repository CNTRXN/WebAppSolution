using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Console.Title = "App";

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "TestApp.Session";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/accessdenied";
                });
            builder.Services.AddAuthorization();
            builder.Services.AddOutputCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseOutputCache();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/error{0}");
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MainPage}/{action=Main}");

            app.MapGet("/logout", async (HttpContext context) => 
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //AppStatics.User = null;

                return Results.Redirect("/");
            });

            /*app.MapGet("/accessdenied", async (HttpContext context) => 
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access Denied");
            });*/

            app.Run();
        }
    }
}