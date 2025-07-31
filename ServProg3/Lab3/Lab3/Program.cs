using Lab3.Data;
using Lab3.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab3;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddTransient<PortfolioService>();
        builder.Services.AddDbContext<AppDbContext>(options =>
                  options.UseSqlite(
                      builder.Configuration.GetConnectionString(
                          "TestimonialsContextSQLite"
                      )
                  )
              );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
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