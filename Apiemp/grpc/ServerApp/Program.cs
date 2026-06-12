using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data.Company;
using ServerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Force HTTP/2 for gRPC
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5090, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

// Register gRPC
builder.Services.AddGrpc();

// MySQL Database
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseMySQL("server=localhost;user=root;password=root;database=Emp"));

var app = builder.Build();

app.MapGrpcService<EmpManagerService>();
app.MapGet("/", () => "Employee gRPC Server Running...");

app.Run();


// using Microsoft.EntityFrameworkCore;
// using ServerApp.Data.Company;
// using ServerApp.Services;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<ShopDbContext>(options =>
//     options.UseMySQL("server=localhost;port=3306;user=root;password=root;database=Emp"));

// builder.Services.AddGrpc();
// var app = builder.Build();

// app.MapGrpcService<EmpManagerService>();
// app.Run();

