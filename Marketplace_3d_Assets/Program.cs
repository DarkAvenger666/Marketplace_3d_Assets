using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Services;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.DataAccess.Repositories;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
/*using MySql.Data.MySqlClient;*/
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace_3d_Assets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MySQLConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var dbServerVersion = new MySqlServerVersion(new Version(8, 0, 36));
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseMySql(connectionString, dbServerVersion));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddTransient<IAssetRepository, AssetRepository>();
            builder.Services.AddTransient<IModerationRepository, ModerationRepository>();
            builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            builder.Services.AddTransient<IFileService, FileService>();
            builder.Services.AddTransient<ITagService, TagService>();
            builder.Services.AddTransient<IAssetTypeService, AssetTypeService>();
            builder.Services.AddTransient<IAssetFileService, AssetFileService>();
            builder.Services.AddTransient<IAssetImageService, AssetImageService>();
            builder.Services.AddTransient<IAssetTagService, AssetTagService>();
            builder.Services.AddTransient<IModerationService, ModerationService>();
            builder.Services.AddTransient<IAssetService, AssetService>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUserService, UserService>();

            /*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();*/
            //builder.Services.AddControllersWithViews();
            /*builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();*/

            builder.Services.AddAuthentication("MyCookieAuth")
                .AddCookie("MyCookieAuth", options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                });

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Marketplace_3d_Assets.PresentationLayer.Controllers.AssemblyMarker).Assembly);
            builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                    {
                        options.ViewLocationFormats.Clear();
                        options.ViewLocationFormats.Add("/PresentationLayer/Views/{1}/{0}.cshtml");
                        options.ViewLocationFormats.Add("/PresentationLayer/Views/Shared/{0}.cshtml");
                    });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                /*app.UseMigrationsEndPoint();*/
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();

            app.Run();
        }
    }
}
