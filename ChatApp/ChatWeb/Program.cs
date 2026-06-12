using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Sales;   // Your proto-generated namespace

var builder = WebApplication.CreateBuilder(args);

// -------------------------------
// Add Razor + gRPC Web
// -------------------------------
builder.Services.AddRazorPages();
builder.Services.AddGrpc();


// -------------------------------
// Add gRPC Clients (MOST IMPORTANT)
// -------------------------------

builder.Services.AddGrpcClient<UserService.UserServiceClient>(o =>
{
    o.Address = new Uri("http://localhost:5090");
})
.ConfigureChannel(o =>
{
    o.HttpHandler = new HttpClientHandler();
    o.HttpVersion = new Version(2, 0);
    o.HttpVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
});

builder.Services.AddGrpcClient<ChatService.ChatServiceClient>(o =>
{
    o.Address = new Uri("http://localhost:5090");
})
.ConfigureChannel(o =>
{
    o.HttpHandler = new HttpClientHandler();
    o.HttpVersion = new Version(2, 0);
    o.HttpVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
});


var app = builder.Build();

// -------------------------------
// Middleware
// -------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Enable gRPC-Web
app.UseGrpcWeb();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
