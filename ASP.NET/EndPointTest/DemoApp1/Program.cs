using Handler;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/Welcome", Greeting.Welcome);
app.Run();