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
using Microsoft.Extensions.FileProviders;

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

            builder.Services.AddScoped<IAssetRepository, AssetRepository>();
            builder.Services.AddScoped<IModerationRepository, ModerationRepository>();
            builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IAssetTypeService, AssetTypeService>();
            builder.Services.AddScoped<IAssetFileService, AssetFileService>();
            builder.Services.AddScoped<IAssetImageService, AssetImageService>();
            builder.Services.AddScoped<IAssetTagService, AssetTagService>();
            builder.Services.AddScoped<IModerationService, ModerationService>();
            builder.Services.AddScoped<IAssetService, AssetService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            /*builder.Services.AddScoped<IUserService, UserService>();*/
            builder.Services.AddScoped<IAssetCommentService, AssetCommentService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IPostTagService, PostTagService>();
            builder.Services.AddScoped<ICartService, CookieCartService>();
            builder.Services.AddScoped<ICheckoutService, CheckoutServiceStub>();
            builder.Services.AddScoped<IModeratorService, ModeratorService>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();

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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), 
                    "PresentationLayer/wwwroot")),
                RequestPath = ""
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "ManageModerators",
                pattern: "ManageModerators/{action=Index}",
                defaults: new { controller = "Moderator"});
            //app.MapRazorPages();

            app.Run();
        }
    }
}
