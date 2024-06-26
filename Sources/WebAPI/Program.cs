
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebAPI.DataContext;
using WebAPI.Services;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var MyPolicy = "MyPolicy";

            // Add services to the container.

            //sql
            builder.Services.AddDbContext<DB_Context>(options =>
            {
                //options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres_Db_College"));
                options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres_Db_Home"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyPolicy,
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            //регистрация сервисов
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IEquipmentService, EquipmentService>();
            builder.Services.AddTransient<ICabinetService, CabinetService>();
            builder.Services.AddTransient<IPermissionService, PermissionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseCors(MyPolicy);
            app.UseStaticFiles(new StaticFileOptions() 
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.MapControllers();

            app.Run();
        }
    }
}