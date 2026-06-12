global using EmpManagerStub = Emp.empManager.empManagerClient;
using ServerApp.Resources;
using ServerApp.security;

var builder = WebApplication.CreateBuilder(args);

// gRPC client
builder.Services.AddGrpcClient<EmpManagerStub>(
    channel => channel.Address = new Uri("http://localhost:5090")
);

// Auth
builder.Services.AddAuthentication()
    .AddJwtBearer(options => JwtHelper.ValidateToken(options));
builder.Services.AddAuthorization();

// ✅ CORS (single policy)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin(); // Node.js, browser, Postman, mobile
    });
});

var app = builder.Build();

// ✅ Apply CORS BEFORE auth
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

// ---------------- ROUTES ----------------

var manager = app.MapGroup("/api/manager");
manager.MapGet("/signin", EmployeeManagerApi.SignIn);

var rest = app.MapGroup("/api/emp");

rest.MapGet("/{EmpId}", EmpControlApi.GetEmp);
rest.MapGet("/", EmpControlApi.GetAllEmp);
rest.MapPost("/", EmpControlApi.AddEmp);

app.Run();











// global using EmpManagerStub = Emp.empManager.empManagerClient;
// using ServerApp.Resources;
// using ServerApp.security;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddGrpcClient<EmpManagerStub>(
//     channel => channel.Address = new Uri("http://localhost:5090")
// );
// builder.Services.AddAuthentication()
//     .AddJwtBearer(options => JwtHelper.ValidateToken(options));
// builder.Services.AddAuthorization();

// builder.Services.AddCors();

// var app = builder.Build();
// app.UseCors();
// app.UseAuthentication();
// app.UseAuthorization();

// var manager = app.MapGroup("/api/manager");


// manager.MapGet("/signin", EmployeeManagerApi.SignIn);

// var rest = app.MapGroup("/api/emp");
// // rest.MapGet("/{EmpId}", EmpControlApi.GetEmp).RequireAuthorization();
// // rest.MapGet("/", EmpControlApi.GetAllEmp).RequireAuthorization();
  
// // rest.MapPost("/", EmpControlApi.AddEmp).RequireAuthorization();
// rest.MapGet("/{EmpId}", EmpControlApi.GetEmp);
// rest.MapGet("/", EmpControlApi.GetAllEmp);
  
// rest.MapPost("/", EmpControlApi.AddEmp);

// manager.RequireCors(policy => policy
//     .WithOrigins("http://localhost:5500", "http://localhost:5001")
//     .AllowAnyMethod()
//     .AllowAnyHeader());

// rest.RequireCors(policy => policy
//     .WithOrigins("http://localhost:5190")
//     .AllowAnyMethod()
//     .AllowAnyHeader()
// );


// app.Run();
