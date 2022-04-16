using Microsoft.AspNetCore.Authentication.Cookies;
using SecondHand.Api.Client.Settings;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SecondHand.Library;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var ClientId = builder.Configuration.GetValue<string>("Strava:ClientId");
var ClientSecret = builder.Configuration.GetValue<string>("Strava:ClientSecret");
builder.Services.Configure<StravaSettings>(builder.Configuration.GetSection(StravaSettings.Key));
builder.Services.AddDbContextFactory<SecondHand.DataAccess.SqlServer.SecondHandContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddSingleton<SecondHand.DataAccess.SqlServer.Api.IAthleteDataAccess, SecondHand.DataAccess.SqlServer.Api.AthleteDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.MongoDB.Api.IAthleteDataAccess, SecondHand.DataAccess.MongoDB.Api.AthleteDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.SqlServer.Api.ITokenExchangeDataAccess, SecondHand.DataAccess.SqlServer.Api.TokenExchangeDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.MongoDB.Api.ITokenExchangeDataAccess, SecondHand.DataAccess.MongoDB.Api.TokenExchangeDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.SqlServer.Api.IAdDataAccess, SecondHand.DataAccess.SqlServer.Api.AdDataAccess>();
builder.Services.AddSingleton<SecondHand.DataAccess.MongoDB.Api.IAdDataAccess, SecondHand.DataAccess.MongoDB.Api.AdDataAccess>();
builder.Services.AddMediatR(typeof(SecondHandLibraryEntryPoint).Assembly);
builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/signin";
            options.LogoutPath = "/signout";
        })
        .AddStrava(options =>
        {
            options.ClientId = ClientId;
            options.ClientSecret = ClientSecret;
        });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".StravaDemo.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.IsEssential = true;
});    


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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



