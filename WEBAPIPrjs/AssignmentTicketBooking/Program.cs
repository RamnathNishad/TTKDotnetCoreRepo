using AssignmentTicketBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentTicketBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //configure connection string to read from appsettings.json into DbContext
            builder.Services.AddSqlServer<TicketDbContext>(builder.Configuration.GetConnectionString("constr"));

            //configure depenedency injection for DAO
            builder.Services.AddScoped<ISeatcingSectionDAO, SeatingSectionDAO>();
 
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }  
}