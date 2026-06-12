using DemoApp.Employee.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SiteModel>();
var app = builder.Build();
app.UseDefaultFiles();
app.MapDefaultControllerRoute();
app.Run();
