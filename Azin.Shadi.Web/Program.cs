using Azin.Shadi.Core.Convertors;
using Azin.Shadi.Core.Services;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Context;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHsts(opts =>
{
    opts.MaxAge = TimeSpan.FromDays(60);
    opts.IncludeSubDomains = true;
    opts.Preload = true;
});



builder.Services.AddMvc();
builder.Services.AddRazorPages();

#region Context
builder.Services.AddDbContext<AzinShadiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzinShadiConectionString"));
});
#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/LogOut";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});
#endregion

#region IOC
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
#endregion



var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
