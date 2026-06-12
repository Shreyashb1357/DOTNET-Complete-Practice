using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using data.chatting;
using ServerApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5090, listen =>
    {
        listen.Protocols = HttpProtocols.Http2;
    });
});


builder.Services.AddGrpc();

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseMySql(
        "server=localhost;database=ChatAppDb;user=root;password=root;",
        new MySqlServerVersion(new Version(8, 0, 34))
    )
);

var app = builder.Build();

app.MapGrpcService<UserServiceImplement>();
app.MapGrpcService<ChatServiceImplement>();

app.Run();
