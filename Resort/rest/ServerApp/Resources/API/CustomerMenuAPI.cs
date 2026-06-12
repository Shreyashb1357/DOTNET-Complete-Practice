using ServerApp.Resources;
using Hotel;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Utils;

namespace ServerApp.Resources.API;

public class CustomerMenuAPI
{
    //  rpc GetFoodCategory(google.protobuf.Empty) returns (FoodCatList);
    public static async Task<IResult> FoodCategoryGet(CustomerMenuProxy remote)
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

    //  rpc GetFoodMenu(google.protobuf.Empty) returns (MenuList);
    public static async Task<IResult> GetMenu(CustomerMenuProxy remote)
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

    //  rpc GetRoom(google.protobuf.Empty) returns (RoomList);
    public static async Task<IResult> GettingRoom(CustomerMenuProxy remote)
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

    //  rpc AddFoodOrder(FoodOrder) returns (ConfirmationCode);
    public static async Task<IResult> FoodOrderAdd(FoodOrder request , CustomerMenuProxy remote)
    {
         try
        {
            var reply = await remote.AddFoodOrderAsync(request);
            var response = new FoodOrderEntry
            {
                CustomerId = request.CustId,
                FoodId = request.FoodId,
                Quantity = request.Quantity
            };
            return Results.Ok(new
            {
                message = reply.ConfirmCode,
                data = response
            });
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Sorry cant place Order!");
        }
    }

    //  rpc RoomBook(BookDetail) returns (BookStatus);
    public static async Task<IResult> BookingRoom(BookDetail request , CustomerMenuProxy remote)
    {
        try
        {
            var reply = await remote.RoomBookAsync(request);
            var response = new RoomBookingEntry
            {
                CustomerId = request.CustId,
                RoomId = request.RoomId
            };
            return Results.Ok(new
            {
                message = reply.ConfirmCode,
                data = response
            });
            
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Sorry cant Book room!");
        }
    }
}