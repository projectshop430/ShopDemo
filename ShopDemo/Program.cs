using Microsoft.EntityFrameworkCore;
using shopDemo.application.Services.implementation;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.Context;
using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IGeneruicRepository<>), typeof(GentericRepository<>));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IPasswordHelper,PasswordHelper>();

builder.Services.AddDbContext<ShopDemoContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ShopDemoConnectionString"));
}, ServiceLifetime.Transient);



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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
