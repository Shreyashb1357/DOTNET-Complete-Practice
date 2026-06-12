using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Data.Resortmanager;
using ServerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Force HTTP/2 for gRPC
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5090, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2; // <-- here
    });
});

// Register gRPC
builder.Services.AddGrpc();

// MySQL Database using official MySQL EF Core provider
builder.Services.AddDbContext<ResortDbContext>(options =>
    options.UseMySQL("server=localhost;user=root;password=root;database=Resort")
);

var app = builder.Build();

app.MapGet("/", () => "Resort gRPC Server Running...");

app.MapGrpcService<ResortManagerService>();
app.MapGrpcService<ReceptionManagerService>();
app.MapGrpcService<CustomerMenuService>();

app.Run();
