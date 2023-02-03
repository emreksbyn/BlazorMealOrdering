using Blazored.Modal;
using BlazorMealOrdering.Server.Data.Context;
using BlazorMealOrdering.Server.Services.Extensions;
using BlazorMealOrdering.Server.Services.Infrastructure;
using BlazorMealOrdering.Server.Services.Managers;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBlazoredModal();

builder.Services.ConfigureMapping();

builder.Services.AddDbContext<MealOrderinDbContext>(config =>
{
    string connectionString = "Server=localhost;Port=5432;Database=MealOrderingDb;User ID=postgres;Password=asd1234;Pooling=true";
    config.UseNpgsql(connectionString);
    config.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ISupplierService, SupplierManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
