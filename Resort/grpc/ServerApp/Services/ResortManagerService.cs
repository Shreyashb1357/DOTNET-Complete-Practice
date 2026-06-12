using Hotel;
using Data.Resortmanager;
using Grpc.Core;
using Grpc.Core.Utils;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.Services;

public class ResortManagerService(ResortDbContext rdb) : ResortManager.ResortManagerBase
{
    // rpc GetAllDept(google.protobuf.Empty) returns (DeptList);

    public override async Task<DeptList> GetAllDept(Empty emt , ServerCallContext context)
    {
        DeptList list = new DeptList();
        var dept = await rdb.departments.ToListAsync();
        foreach(var entry in dept)
        {
            list.Departments.Add(new Hotel.Department
            {
                DeptId = entry.departmentId,
                DeptName = entry.DName,
                Description = entry.description
            });
        }

        return list;
    }

    //rpc GetRoom(google.protobuf.Empty) returns (RoomList);

    public override async Task<RoomList> GetRoom(Empty request , ServerCallContext context)
    {
        RoomList list = new RoomList();
        var room = await rdb.rooms.ToListAsync();
        foreach(var entry in room)
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

    //  rpc GetCust(google.protobuf.Empty) returns (CustList);

    public override async Task<CustList> GetCust(Empty request , ServerCallContext context)
    {
        CustList list = new CustList();
        var cust = await rdb.customers.ToListAsync();

        foreach(var entry in cust)
        {
            list.Customers.Add(new Hotel.Customer
            {
                CustId = entry.CustId,
                CustName = entry.CustName,
                Phone = entry.phone,
                Email = entry.email,
                Address = entry.address
            });
        }
        return list;
    }

    //  rpc GetFoodCategory(google.protobuf.Empty) returns (FoodCatList);
    public override async Task<FoodCatList> GetFoodCategory(Empty request , ServerCallContext context)
    {
        FoodCatList list = new FoodCatList();
        var food = await rdb.foodcategories.ToListAsync();
        foreach(var entry in food)
        {
            list.Categories.Add(new Hotel.FoodCategory
            {
                CategoryId = entry.FoodCategoryId,
                CategoryName = entry.FoodCategoryName
            });
        }
        return list;
    }

    //  rpc GetFoodMenu(google.protobuf.Empty) returns (MenuList);
    public override async Task<MenuList> GetFoodMenu(Empty request , ServerCallContext context)
    {
        MenuList list = new MenuList();
        var menu = await rdb.foodmenu.ToListAsync();
        foreach(var entry in menu)
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

    // rpc GetOrders(google.protobuf.Empty) returns (OrderList);

    public override async Task<OrderList> GetOrders(Empty request , ServerCallContext context) {
        OrderList list = new OrderList();
        var orders = await rdb.foodorders.ToListAsync();
        foreach(var entry in orders)
        {
            list.Orders.Add(new Hotel.Order
            {
                OrderId = entry.orderId,
                CustId = entry.CustId,
                FoodId = entry.foodId,
                Quantity = entry.quantity,
                TotalPrice = entry.totalPrice,
                //OrderDate = entry.orderDate
                OrderDate = Timestamp.FromDateTime(
                    entry.orderDate.ToUniversalTime())
            });
        }
        return list;
    }

    //  rpc GetBooking(google.protobuf.Empty) returns (BookingList);
    public override async Task<BookingList> GetBooking(Empty request , ServerCallContext context)
{
    BookingList list = new BookingList();

    var bookings = await rdb.roombookings.ToListAsync();

    foreach(var entry in bookings)
    {
        var room = await rdb.rooms.FirstOrDefaultAsync(r => r.rId == entry.rId);

        int totalDays = (entry.CheckOut - entry.CheckIn).Days;
        double totalPayment = 0;

        if(room != null && totalDays > 0)
        {
            totalPayment = totalDays * room.Price;
        }

        list.Bookings.Add(new Hotel.Booking
        {
            BookingId = entry.BId,
            CustId = entry.CustId,
            RoomId = entry.rId,
            CheckIn = Timestamp.FromDateTime(entry.CheckIn.ToUniversalTime()),
            CheckOut = Timestamp.FromDateTime(entry.CheckOut.ToUniversalTime()),
            TotalPayment = totalPayment
        });
    }

    return list;
}

}