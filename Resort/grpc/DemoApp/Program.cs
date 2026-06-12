using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using Hotel;

class Program
{
    static async Task Main(string[] args)
    {
        AppContext.SetSwitch(
            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

        using var channel = GrpcChannel.ForAddress("http://localhost:5090");
        var client = new ResortManager.ResortManagerClient(channel);

        while (true)
        {
            Console.WriteLine("\n===== RESORT MANAGEMENT =====");
            Console.WriteLine("1. View All Departments");
            Console.WriteLine("2. View All Rooms");
            Console.WriteLine("3. View All Customers");
            Console.WriteLine("4. View Food Categories");
            Console.WriteLine("5. View Food Menu");
            Console.WriteLine("6. View All Orders");
            Console.WriteLine("7. View All Bookings");
            Console.WriteLine("8. Exit");

            Console.Write("Enter choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ViewDepartments(client);
                    break;

                case "2":
                    await ViewRooms(client);
                    break;

                case "3":
                    await ViewCustomers(client);
                    break;

                case "4":
                    await ViewFoodCategories(client);
                    break;

                case "5":
                    await ViewFoodMenu(client);
                    break;

                case "6":
                    await ViewOrders(client);
                    break;

                case "7":
                    await ViewBookings(client);
                    break;

                case "8":
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    // ================= METHODS =================

    static async Task ViewDepartments(ResortManager.ResortManagerClient client) 
    {
        var response = await client.GetAllDeptAsync(new Empty());
        Console.WriteLine("\n--- Departments ---");
        foreach (var d in response.Departments)
        {
            Console.WriteLine($"{d.DeptId} | {d.DeptName} | {d.Description}");
        }
    }

    static async Task ViewRooms(ResortManager.ResortManagerClient client)
    {
        var response = await client.GetRoomAsync(new Empty());
        Console.WriteLine("\n--- Rooms ---");
        foreach (var r in response.Rooms)
        {
            Console.WriteLine(
                $"RoomId: {r.RoomId}, Type: {r.RoomType}, Price: {r.PricePerNight}, Status: {r.Status}");
        }
    }

    static async Task ViewCustomers(ResortManager.ResortManagerClient client)
    {
        var response = await client.GetCustAsync(new Empty());
        Console.WriteLine("\n--- Customers ---");
        foreach (var c in response.Customers)
        {
            Console.WriteLine(
                $"{c.CustId} | {c.CustName} | {c.Phone} | {c.Email}");
        }
    }

    static async Task ViewFoodCategories(ResortManager.ResortManagerClient client)
    {
        var response = await client.GetFoodCategoryAsync(new Empty());
        Console.WriteLine("\n--- Food Categories ---");
        foreach (var f in response.Categories)
        {
            Console.WriteLine($"{f.CategoryId} | {f.CategoryName}");
        }
    }

    static async Task ViewFoodMenu(ResortManager.ResortManagerClient client)
    {
        var response = await client.GetFoodMenuAsync(new Empty());
        Console.WriteLine("\n--- Food Menu ---");
        foreach (var m in response.Menu)
        {
            Console.WriteLine(
                $"{m.FoodId} | {m.FoodName} | ₹{m.Price} | Category: {m.CategoryId}");
        }
    }

    static async Task ViewOrders(ResortManager.ResortManagerClient client)
    {
        var response = await client.GetOrdersAsync(new Empty());
        Console.WriteLine("\n--- Orders ---");
        foreach (var o in response.Orders)
        {
            Console.WriteLine(
                $"OrderId: {o.OrderId}, CustId: {o.CustId}, FoodId: {o.FoodId}, Qty: {o.Quantity}, Total: {o.TotalPrice}, Date: {o.OrderDate.ToDateTime()}");
        }
    }

    static async Task ViewBookings(ResortManager.ResortManagerClient client)
    {
        var response = await client.GetBookingAsync(new Empty());
        Console.WriteLine("\n--- Bookings ---");
        foreach (var b in response.Bookings)
        {
            var checkInDate = b.CheckIn.ToDateTime();   // convert Timestamp -> DateTime
            var checkOutDate = b.CheckOut.ToDateTime();

            Console.WriteLine($"BookingId: {b.BookingId}, CustId: {b.CustId}, RoomId: {b.RoomId}, " +
                            $"CheckIn: {checkInDate:MM/dd/yyyy}, CheckOut: {checkOutDate:MM/dd/yyyy} , Total Payment: {b.TotalPayment}");
        }
    }
}
