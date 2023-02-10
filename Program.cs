using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.Areas.Identity.Data;
using ProjectoFinal_LR.Data;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Services.Animals;
using ProjectoFinal_LR.Services.Appointments;
using ProjectoFinal_LR.Services.Breeds;
using ProjectoFinal_LR.Services.Clients;
using ProjectoFinal_LR.Services.Motives;
using ProjectoFinal_LR.Services.Priorities;
using ProjectoFinal_LR.Services.Species;
using ProjectoFinal_LR.Services.Veterinarians;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Text.Json.Serialization;

namespace ProjectoFinal_LR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            string connectionStr = configuration.GetConnectionString("DbContextConnection");

            builder.Services.AddDbContext<MainDbContext>(option =>
               option.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr), options =>
               {
                   options.EnableRetryOnFailure(
                       maxRetryCount: 3,
                       maxRetryDelay: System.TimeSpan.FromSeconds(1),
                       errorNumbersToAdd: null);
               }));

            builder.Services.AddDbContext<IdentityDbContext>(option =>
                option.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr), options =>
                {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: System.TimeSpan.FromSeconds(1),
                        errorNumbersToAdd: null);
                }));
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
           .AddEntityFrameworkStores<IdentityDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
            });
            builder.Services.AddScoped<IAnimalsServices, AnimalsServices>();
            builder.Services.AddScoped<IClientsServices, ClientsServices>();
            builder.Services.AddScoped<ISpeciesServices, SpeciesServices>();
            builder.Services.AddScoped<IBreedsServices, BreedsServices>();
            builder.Services.AddScoped<IAppointmentsServices, AppointmentsServices>();
            builder.Services.AddScoped<IPrioritiesServices, PrioritiesServices>();
            builder.Services.AddScoped<IMotivesServices, MotivesServices>();
            builder.Services.AddScoped<IVeterinariansServices, VeterinariansServices>();

            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true; // adiciona um close button à toast notification
                config.Position = NotyfPosition.BottomRight;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); ;

            app.UseAuthorization();

            app.UseNotyf();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}