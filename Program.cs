using Microsoft.EntityFrameworkCore;
using TestingDBVenta.Models;
using TestingDBVenta.Models.Implementations;
using TestingDBVenta.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbventaContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));


// Generic Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IVentaRepository, VentaRepository>();

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
