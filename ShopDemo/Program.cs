using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using shopDemo.application.Services.implementation;
using shopDemo.application.Services.Interface;
using ShopDemo.Data.Context;
using ShopDemo.Data.Entity.Account;
using ShopDemo.Data.Repository;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
#region Services
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGeneruicRepository<>), typeof(GentericRepository<>));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IPasswordHelper,PasswordHelper>();
builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<ISmsService, SmsService>();
#endregion
#region Database
builder.Services.AddDbContext<ShopDemoContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ShopDemoConnectionString"));
}, ServiceLifetime.Transient);

#endregion
#region authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme=CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme=CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(option=>
    {
        option.LoginPath = "/login";
        option.LoginPath = "/log-out";
        option.ExpireTimeSpan = TimeSpan.FromDays(30);
        
}) ;


#endregion
#region html encoder
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
