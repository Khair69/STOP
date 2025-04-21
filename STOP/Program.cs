using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using STOP.Authorization.Handlers;
using STOP.Authorization.Policies;
using STOP.Data;
using STOP.Models;
using STOP.Services;

namespace STOP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization(options => {
                options.AddPolicy("CanManageDog", policyBuilder =>
                policyBuilder.AddRequirements(new IsDogOwnerRequirement()));

                options.AddPolicy(
                    "CanDeleteDog",
                    policyBuilder => policyBuilder.AddRequirements(
                        new CanDeleteDogRequirement()));

                options.AddPolicy(
                    "IsAdmin",
                    policyBuilder => policyBuilder.RequireClaim("IsAdmin"));
            });

            builder.Services.AddScoped<IAuthorizationHandler, IsDogOwnerHandler>();
            builder.Services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, IsDogOwnerDelHandler>();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => { 
                options.SignIn.RequireConfirmedAccount = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IDogService, DogService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
