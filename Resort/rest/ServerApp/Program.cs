global using ResortManagerProxy = Hotel.ResortManager.ResortManagerClient;
global using ReceptionManagerProxy = Hotel.ReceptionManager.ReceptionManagerClient;
global using CustomerMenuProxy = Hotel.CustomerMenu.CustomerMenuClient;
using ServerApp.Resources.API;
using ServerApp.Resources.Converter;
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new ServerApp.Resources.Converter.TimestampConverter());
});

var grpcServer = new Uri("http://localhost:5090");
builder.Services.AddGrpcClient<ResortManagerProxy>(channel => channel.Address = grpcServer);
builder.Services.AddGrpcClient<ReceptionManagerProxy>(channel => channel.Address = grpcServer);
builder.Services.AddGrpcClient<CustomerMenuProxy>(channel => channel.Address = grpcServer);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();
app.UseCors("AllowFrontend");

var rest = app.MapGroup("/api/resort");

// ---------- Resort Manager ----------
rest.MapGet("/departments", ResortManagerAPI.GetDept);
rest.MapGet("/rooms", ResortManagerAPI.RoomGet);
rest.MapGet("/customers", ResortManagerAPI.CustGet);
rest.MapGet("/foodcategories", ResortManagerAPI.GetFoodCat);
rest.MapGet("/foodmenu", ResortManagerAPI.FoodMenuGet);
rest.MapGet("/orders", ResortManagerAPI.OrderGet);
rest.MapGet("/bookings", ResortManagerAPI.GetBook);

// ---------- Reception Manager ----------
rest.MapPost("/customer/add", ReceptionManagerAPI.CustAdd);
rest.MapPost("/foodorder/add", ReceptionManagerAPI.PlaceFoodOrder);
rest.MapPost("/roombook", ReceptionManagerAPI.BookRoom);

// ---------- Customer Menu ----------
rest.MapGet("/menu/categories", CustomerMenuAPI.FoodCategoryGet);
rest.MapGet("/menu/items", CustomerMenuAPI.GetMenu);
rest.MapGet("/menu/rooms", CustomerMenuAPI.GettingRoom);
rest.MapPost("/menu/order", CustomerMenuAPI.FoodOrderAdd);
rest.MapPost("/menu/roombook", CustomerMenuAPI.BookingRoom);

app.Run();
