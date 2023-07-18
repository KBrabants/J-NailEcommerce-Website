using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebSite.Data;
using MyWebSite.Filters;
using MyWebSite.Middleware;
using MyWebSite.Models;
using MyWebSite.RouteConstraints;
using MyWebSite.Services;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    options.Filters.Add<ResourceLoggingFilter>();
});
builder.Services.AddControllers();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();
builder.Services.AddTransient<JsonFileInfoService>();
builder.Services.AddScoped<CookieCartService>();
builder.Services.AddTransient<NailQueryingService>();

//builder.Services.AddDbContext<NailStoreContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString
//  ("Nailsdb"));
//});
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("category", typeof(CategoryConstraints));
});
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




if (!app.Environment.IsDevelopment())
{
    // Used
    app.Environment.WebRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +"/var/MyApp/wwwroot";

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        RequestPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
    });
    app.UsePathBase(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

    app.UseExceptionHandler("/error");
}
else
{
    app.UseStaticFiles(new StaticFileOptions
    {
        RequestPath = ""
    });
    app.UsePathBase("/");
}

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}");
app.UseSession();


app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();


app.Run();

