using ServerApp.Resources;
using Hotel;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Utils;

namespace ServerApp.Resources.API;

public class ResortManagerAPI 
{
    public static async Task<IResult> GetDept(ResortManagerProxy remote)
    {
        try{
            var reply = await remote.GetAllDeptAsync(new Empty());
            var result = reply.Departments.Select(d=> new DepartmentEntry
            {
                DeptId = d.DeptId,
                DeptName = d.DeptName   ,
                description = d.Description            
            });
            return Results.Ok(result);
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("No department exists");
        }
    }
    
    public static async Task<IResult> RoomGet(ResortManagerProxy remote)
    {
        try
        {
            var reply = await remote.GetRoomAsync(new Empty());
            var result = reply.Rooms.Select(r => new RoomEntry
            {
                RoomId = r.RoomId,
                RoomNumber = r.RoomNumber,
                RoomType = r.RoomType,
                Price = r.PricePerNight,
                DeptId = r.DeptId
            });
            return Results.Ok(result);
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Rooms not exits..");
        }
    }

    public static async Task<IResult> CustGet(ResortManagerProxy remote)
    {
        try
        {
            var reply = await remote.GetCustAsync(new Empty());
            var result = reply.Customers.Select(c => new CustomerEntry
            {
                CustId = c.CustId,
                CustName = c.CustName,
                phone = c.Phone,
                email = c.Email,
                address = c.Address
            });
            return Results.Ok(result);
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Customer not exists");
        }
    }

    public static async Task<IResult> GetFoodCat(ResortManagerProxy remote)
    {
        try
        {
            var reply = await remote.GetFoodCategoryAsync(new Empty());
            var result = reply.Categories.Select(r => new FoodCategoryEntry
            {
                CategoryId = r.CategoryId,
                CategoryName = r.CategoryName
            });
            return Results.Ok(result);
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Food Category not exits..");
        }
    }

    public static async Task<IResult> FoodMenuGet(ResortManagerProxy remote)
    {
        try
        {
            var reply = await remote.GetFoodMenuAsync(new Empty());
            var result = reply.Menu.Select(r => new FoodMenuEntry
            {
                FoodId = r.FoodId,
                FoodName = r.FoodName,
                Price = r.Price,
                CategoryId = r.CategoryId
            });
            return Results.Ok(result);
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Food Menu not exits..");
        }
    }

    public static async Task<IResult> OrderGet(ResortManagerProxy remote)
    {
        try
        {
            var reply = await remote.GetOrdersAsync(new Empty());
            var result = reply.Orders.Select(r => new FoodOrderEntry
            {
                OrderId = r.OrderId,
                CustomerId = r.CustId,
                FoodId = r.FoodId,
                Quantity = r.Quantity,
                TotalPrice = r.TotalPrice,
                //OrderDate = Timestamp.FromDateTime(r.OrderDate.ToUniversalTime())
                OrderDate = r.OrderDate.ToDateTime()
            });
            return Results.Ok(result);
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Food Order not exits..");
        }
    }

    public static async Task<IResult> GetBook(ResortManagerProxy remote)
    {
        try
        {
            var reply = await remote.GetBookingAsync(new Empty());
            var result = reply.Bookings.Select(r => new RoomBookingEntry
            {
                BookingId = r.BookingId,
                CustomerId = r.CustId,
                RoomId = r.RoomId,
                CheckIn = r.CheckIn.ToDateTime(),
                CheckOut = r.CheckOut.ToDateTime(),
                TotalPayment = r.TotalPayment
            });
            return Results.Ok(result);
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Room Booking not exits..");
        }
    }
}