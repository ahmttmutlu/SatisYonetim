using SatisYonetim.Repositories;

namespace SatisYonetim
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connectionString = "Server=DESKTOP-CB6V4KQ\\SQLEXPRESS; Database=SatisYonetim; Integrated Security=True; TrustServerCertificate=Yes";//her yerden eri�im sa�lamak i�in buraya da ekledim
            builder.Services.AddSingleton(new DurumRepository(connectionString));

            builder.Services.AddSingleton(new TeklifRepository(connectionString));


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Teklif}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
