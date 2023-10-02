using Microsoft.AspNetCore.Authentication.Cookies;
using MyFirstMVCoreApp.Models;

namespace MyFirstMVCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //configure the DbContext for Sql Server connection string
            //read the connection string from appsettings.json file using ConfigurationManager class

            var configMgr = builder.Configuration;
            var constr = configMgr.AddJsonFile("appsettings.json")
                                  .Build()
                                  .GetConnectionString("constr");
            
            builder.Services.AddSqlServer<EmpDbContext>(constr);

            //configure the dependency injection for the DataAccessLayer class
            builder.Services.AddTransient<IEmpDataAccess,EmpDataAccess>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.Cookie.Name = ".AspNetCore.Identity.Application";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.ReturnUrlParameter = "ReturnUrl";
                });

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            //check for every request whether user is authenticated or not
            app.Use(async (context, next) =>
            {
                //get the token from the environment (Session)
                var isValid = context.Session.GetString("IsAuthenticated");
                //send the request again with bearer token
                if (!string.IsNullOrEmpty(isValid) && isValid=="true")
                {
                    await next();
                }                
            });


            app.UseAuthentication(); //must be before UseAuthorization()
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}