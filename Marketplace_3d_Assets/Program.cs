using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Text;

namespace Marketplace_3d_Assets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MySQLConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseMySQL(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            /*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();*/
            //builder.Services.AddControllersWithViews();
            /*builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();*/

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
            /*using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM asset", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetGuid("id"));
                        //reader.GetTime
                        //reader.Get
                    }
                }
            }*/

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            /*app.UseAuthorization();*/

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();

            /*app.Run(async (context) =>
            {
                if (context.Request.Cookies.ContainsKey("name"))
                {
                    string? name = context.Request.Cookies["name"];
                    await context.Response.WriteAsync($"Hello {name}!");
                }
                else
                {
                    context.Response.Cookies.Append("name", "Tom");
                    await context.Response.WriteAsync("Hello World!");
                }
            });*/

            app.Run();
        }
    }
}
