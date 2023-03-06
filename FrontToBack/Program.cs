using FrontToBack;
using FrontToBack.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var _config = builder.Configuration;



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
});
builder.Services.FrontToBackServiceRegistration();
builder.Services.AddSession(options=>options.IdleTimeout= TimeSpan.FromSeconds(15));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
        );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
