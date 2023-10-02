using EFCoreCRUDLib;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace EmployeeWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {                
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //configure SQL Server Connection String
            var constr = builder.Configuration
                                .AddJsonFile("appsettings.json")
                                .Build()
                                .GetConnectionString("constr");
            builder.Services.AddSqlServer<EmpDbContext>(constr, options => options.EnableRetryOnFailure());

            //configure Dependency Injection for DataAccessLayer class
            builder.Services.AddTransient<IEmpDataAccess, EmpDataAccessLayer>();

            //enable CORS policy for clients access
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("clients-allowed", opt =>
                {
                    opt.WithOrigins("http://localhost:5291")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            //use the CORS policy
            app.UseCors("clients-allowed");

            app.Run();
        }
    }
}