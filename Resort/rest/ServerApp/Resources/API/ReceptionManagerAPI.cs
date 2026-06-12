using ServerApp.Resources;
using Hotel;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Globalization;
using ServerApp.Resources.Converter;
using Grpc.Core.Utils;

namespace ServerApp.Resources.API;


public class ReceptionManagerAPI
{
    //  rpc AddCust(Customer) returns (AddStatus);
    // public static async Task<IResult> CustAdd(Customer request , ReceptionManagerProxy remote)
    // {
    //     try
    //     {
    //         var reply = await remote.AddCustAsync(request);
    //         var response = new CustomerEntry
    //         {
    //             CustId = request.CustId,
    //             CustName = request.CustName,
    //             phone = request.Phone,
    //             email = request.Email,
    //             address = request.Address
    //         };
    //         return Results.Ok(new
    //         {
    //             message = reply.ConfirmCode,
    //             data = response
    //         });
            
    //     }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
    //     {
    //         return Results.NotFound("Sorry cant add Customer");
    //     }
    // }

    public static async Task<IResult> CustAdd(Customer request , ReceptionManagerProxy remote)
    {
        try
        {
            var reply = await remote.AddCustAsync(request);

            int custId = int.Parse(reply.ConfirmCode);

            return Results.Ok(new
            {
                CustId = custId,
                Message = "Customer Added Successfully"
            });
        }
        catch (RpcException ex)
        {
            return Results.Problem(ex.Status.Detail);
        }
    }



    //  rpc AddFoodOrder(FoodOrder) returns (ConfirmationCode);

    // public static async Task<IResult> PlaceFoodOrder(FoodOrder request , ReceptionManagerProxy remote)
    // {
    //     try
    //     {
    //         var reply = await remote.AddFoodOrderAsync(request);
    //         var response = new FoodOrderEntry
    //         {
    //             CustomerId = request.CustId,
    //             FoodId = request.FoodId,
    //             Quantity = request.Quantity
    //         };
    //         return Results.Ok(new
    //         {
    //             message = reply.ConfirmCode,
    //             data = response
    //         });
            
    //     }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
    //     {
    //         return Results.NotFound("Sorry cant place Order!");
    //     }
    // }

    public static async Task<IResult> PlaceFoodOrder(FoodOrder request , ReceptionManagerProxy remote)
    {
        var reply = await remote.AddFoodOrderAsync(request);

        return Results.Ok(new
        {
            message = reply.ConfirmCode
        });
    }




    //  rpc RoomBook(BookDetail) returns (BookStatus);
    // public static async Task<IResult> BookRoom(BookDetail request , ReceptionManagerProxy remote)
    // {
    //     try
    //     {
    //         var reply = await remote.RoomBookAsync(request);
    //         var response = new RoomBookingEntry
    //         {
    //             CustomerId = request.CustId,
    //             RoomId = request.RoomId
    //         };
    //         return Results.Ok(new
    //         {
    //             message = reply.ConfirmCode,
    //             data = response
    //         });
            
    //     }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
    //     {
    //         return Results.NotFound("Sorry cant Book room!");
    //     }
    // }

    public static async Task<IResult> BookRoom(BookDetail request , ReceptionManagerProxy remote)
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
        }
        catch (RpcException ex)
        {
            return Results.Problem(ex.Status.Detail);
        }
    }


}