using Microsoft.EntityFrameworkCore;
using Veluxe.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VeluxeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VeluxeConnection")));

//Implememting sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Veluxe.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

//Using middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//Enabling session
app.UseSession();

app.UseAuthorization();

//Mapping routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
