using Hotel;
using Data.Resortmanager;
using Grpc.Core;
using Grpc.Core.Utils;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.Services;

public class CustomerMenuService(ResortDbContext rdb) : CustomerMenu.CustomerMenuBase
{
    //rpc GetFoodCategory(google.protobuf.Empty) returns (FoodCatList);

    public override async Task<FoodCatList> GetFoodCategory(Empty request, ServerCallContext context)
    {
        FoodCatList list = new FoodCatList();
        var cat = await rdb.foodcategories.ToListAsync();
        foreach (var entry in cat)
        {
            list.Categories.Add(new Hotel.FoodCategory
            {
                CategoryId = entry.FoodCategoryId,
                CategoryName = entry.FoodCategoryName
            });
        }
        return list;
    }

    // rpc GetFoodMenu(google.protobuf.Empty) returns (MenuList);
    public override async Task<MenuList> GetFoodMenu(Empty request, ServerCallContext context)
    {
        MenuList list = new MenuList();
        var menu = await rdb.foodmenu.ToListAsync();
        foreach (var entry in menu)
        {
            list.Menu.Add(new Hotel.FoodMenu
            {
                FoodId = entry.foodId,
                FoodName = entry.foodName,
                Price = entry.price,
                CategoryId = entry.FoodCategoryId
            });
        }
        return list;
    }

    // rpc GetRoom(google.protobuf.Empty) returns (RoomList);

    public override async Task<RoomList> GetRoom(Empty request, ServerCallContext context)
    {
        RoomList list = new RoomList();
        var room = await rdb.rooms.ToListAsync();
        foreach (var entry in room)
        {
            list.Rooms.Add(new Hotel.Room
            {
                RoomId = entry.rId,
                RoomNumber = entry.rId,
                RoomType = entry.RoomType,
                PricePerNight = entry.Price,
                //Status = (Hotel.RoomStatus)entry.status,
                Status = entry.status switch
                {
                    "Available"   => Hotel.RoomStatus.Available,
                    "Occupied"    => Hotel.RoomStatus.Occupied,
                    "Maintenance" => Hotel.RoomStatus.Maintenance,
                    _             => Hotel.RoomStatus.Available
                },
                DeptId = entry.departmentId
            });
        }
        return list;
    }


    //  rpc AddFoodOrder(FoodOrder) returns (ConfirmationCode);

    public override async Task<ConfirmationCode> AddFoodOrder(Hotel.FoodOrder request, ServerCallContext context)
    {
        try
        {
            var customerExists = await rdb.customers.AnyAsync(c => c.CustId == request.CustId);
            var fid = await rdb.foodmenu.FirstOrDefaultAsync(c => c.foodId == request.FoodId);
            if (!customerExists)
            {
                return new ConfirmationCode
                {
                    ConfirmCode="Customer doesnt exist!!"
                };
            }
            else if (fid == null)
            {
                return new ConfirmationCode
                {
                    ConfirmCode = "Food item doesn't exist!!"
                };
            }
            else
            {
                var totalPrice = fid.price * request.Quantity;
                var order = new Data.Resortmanager.FoodOrder
                {
                    CustId = request.CustId,
                    foodId = request.FoodId,
                    quantity = request.Quantity,     
                    totalPrice = totalPrice            
                };
                rdb.foodorders.Add(order);
                await rdb.SaveChangesAsync();
                return new ConfirmationCode
                {
                    ConfirmCode="Order placed!!"
                };
            }

        }
        catch (Exception)
        {
            context.Status = new Status(StatusCode.Internal , "Food Order failed due to the internal exception!!");
            return new ConfirmationCode{ConfirmCode="Failed to Place Order"};
        }
    }


   //rpc RoomBook(BookDetail) returns (BookStatus);
    public override async Task<BookStatus> RoomBook(BookDetail request, ServerCallContext context)
    {
        try
        {
            if (request.CheckIn == null || request.CheckOut == null)
            {
                return new BookStatus
                {
                    ConfirmCode = "Check-in and Check-out dates are required"
                };
            }

            var customerExists = await rdb.customers
                .AnyAsync(c => c.CustId == request.CustId);

            if (!customerExists)
            {
                return new BookStatus { ConfirmCode = "Customer does not exist" };
            }

            var room = await rdb.rooms
                .FirstOrDefaultAsync(r => r.rId == request.RoomId);

            if (room == null)
            {
                return new BookStatus { ConfirmCode = "Room does not exist" };
            }

            var checkIn = request.CheckIn.ToDateTime();
            var checkOut = request.CheckOut.ToDateTime();

            int days = (checkOut.Date - checkIn.Date).Days;

            if (days <= 0)
            {
                return new BookStatus
                {
                    ConfirmCode = "Check-out must be after check-in"
                };
            }

            var isBooked = await rdb.roombookings.AnyAsync(b =>
                b.rId == request.RoomId &&
                checkIn < b.CheckOut &&
                checkOut > b.CheckIn
            );

            if (isBooked)
            {
                return new BookStatus
                {
                    ConfirmCode = "Room already booked for selected dates"
                };
            }

            double totalAmount = days * room.Price;

            var booking = new Data.Resortmanager.RoomBooking
            {
                CustId = request.CustId,
                rId = request.RoomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                totalPayment = totalAmount
            };

            rdb.roombookings.Add(booking);
            await rdb.SaveChangesAsync();

            return new BookStatus
            {
                ConfirmCode = "Room booked successfully"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            context.Status = new Status(StatusCode.Internal, ex.Message);

            return new BookStatus
            {
                ConfirmCode = "Booking failed"
            };
        }
    }



}


