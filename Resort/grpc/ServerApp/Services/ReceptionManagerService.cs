using Hotel;
using Data.Resortmanager;
using Grpc.Core;
using Grpc.Core.Utils;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.Services;

public class ReceptionManagerService(ResortDbContext rdb) : ReceptionManager.ReceptionManagerBase
{
    //  rpc AddCust(Customer) returns (AddStatus);
    // public override async Task<AddStatus> AddCust(Hotel.Customer request , ServerCallContext context)
    // {
    //     try{
    //         var cust = await rdb.customers.FindAsync(request.CustId);
    //         if(cust == null)
    //         {
    //             var custmer = new Data.Resortmanager.Customer
    //             {
    //                 //CustId = request.CustId,
    //                 CustName = request.CustName,
    //                 phone = request.Phone,
    //                 email = request.Email,
    //                 address = request.Address
    //             };
    //             rdb.customers.Add(custmer);
    //             await rdb.SaveChangesAsync();
    //             return new AddStatus
    //             {
    //                 ConfirmCode = "Customer Added Successfully!"
    //             };
    //         }
    //         else
    //         {
    //             return new AddStatus
    //             {
    //                 ConfirmCode = "Customer Already exists"
    //             };
    //         }
    //     }catch(Exception )
    //     {
    //         context.Status = new Status(StatusCode.Internal , "Customer addition failed due to the internal exception!!");
    //         return new AddStatus{ConfirmCode="Failed to add"};
    //     }
            
    // }

    public override async Task<AddStatus> AddCust(Hotel.Customer request , ServerCallContext context)
    {
        try
        {
            var custmer = new Data.Resortmanager.Customer
            {
                CustName = request.CustName,
                phone = request.Phone,
                email = request.Email,
                address = request.Address
            };

            rdb.customers.Add(custmer);
            await rdb.SaveChangesAsync();

            return new AddStatus
            {
                ConfirmCode = custmer.CustId.ToString()
            };
        }
        catch (Exception)
        {
            context.Status = new Status(StatusCode.Internal, "Customer addition failed");
            return new AddStatus { ConfirmCode = "0" };
        }
    }


    //  rpc AddFoodOrder(FoodOrder) returns (ConfirmationCode);
    // public override async Task<ConfirmationCode> AddFoodOrder(Hotel.FoodOrder request , ServerCallContext context)
    // {
    //     try
    //     {
    //         var customerExists = await rdb.customers.AnyAsync(c => c.CustId == request.CustId);
    //         var fid = await rdb.foodmenu.FirstOrDefaultAsync(c => c.foodId == request.FoodId);
    //         if (!customerExists)
    //         {
    //             return new ConfirmationCode
    //             {
    //                 ConfirmCode="Customer doesnt exist!!"
    //             };
    //         }
    //         else if (fid == null)
    //         {
    //             return new ConfirmationCode
    //             {
    //                 ConfirmCode = "Food item doesn't exist!!"
    //             };
    //         }
    //         else
    //         {
    //             var totalPrice = fid.price * request.Quantity;
    //             var order = new Data.Resortmanager.FoodOrder
    //             {
    //                 CustId = request.CustId,
    //                 foodId = request.FoodId,
    //                 quantity = request.Quantity,     
    //                 totalPrice = totalPrice            
    //             };
    //             rdb.foodorders.Add(order);
    //             await rdb.SaveChangesAsync();
    //             return new ConfirmationCode
    //             {
    //                 ConfirmCode="Order placed!!"
    //             };
    //         }

    //     }
    //     catch (Exception)
    //     {
    //         context.Status = new Status(StatusCode.Internal , "Food Order failed due to the internal exception!!");
    //         return new ConfirmationCode{ConfirmCode="Failed to Place Order"};
    //     }
    // }

    public override async Task<ConfirmationCode> AddFoodOrder(Hotel.FoodOrder request, ServerCallContext context)
    {
        try
        {
            int custId = request.CustId;

            if (custId == 0)
            {
                var lastCustomer = await rdb.customers
                    .OrderByDescending(c => c.CustId)
                    .FirstOrDefaultAsync();

                if (lastCustomer == null)
                {
                    return new ConfirmationCode
                    {
                        ConfirmCode = "Customer doesnt exist!!"
                    };
                }

                custId = lastCustomer.CustId;
            }

            var fid = await rdb.foodmenu
                .FirstOrDefaultAsync(f => f.foodId == request.FoodId);

            if (fid == null)
            {
                return new ConfirmationCode
                {
                    ConfirmCode = "Food item doesn't exist!!"
                };
            }

            var totalPrice = fid.price * request.Quantity;

            var order = new Data.Resortmanager.FoodOrder
            {
                CustId = custId,
                foodId = request.FoodId,
                quantity = request.Quantity,
                totalPrice = totalPrice
            };

            rdb.foodorders.Add(order);
            await rdb.SaveChangesAsync();

            return new ConfirmationCode
            {
                ConfirmCode = "Order placed!!"
            };
        }
        catch (Exception)
        {
            context.Status = new Status(StatusCode.Internal, "Food Order failed");
            return new ConfirmationCode { ConfirmCode = "Failed to Place Order" };
        }
    }



    //  rpc RoomBook(BookDetail) returns (BookStatus);
    // public override async Task<BookStatus> RoomBook(BookDetail request, ServerCallContext context)
    // {
    //     try
    //     {
    //         var customerExists = await rdb.customers.AnyAsync(c => c.CustId == request.CustId);

    //         if (!customerExists)
    //         {
    //             return new BookStatus
    //             {
    //                 ConfirmCode = "Customer does not exist"
    //             };
    //         }

    //         var room = await rdb.rooms.FirstOrDefaultAsync(r => r.rId == request.RoomId);

    //         if (room == null)
    //         {
    //             return new BookStatus
    //             {
    //                 ConfirmCode = "Room does not exist"
    //             };
    //         }

    //         var checkIn = request.CheckIn.ToDateTime();
    //         var checkOut = request.CheckOut.ToDateTime();

    //         if (checkOut <= checkIn)
    //         {
    //             return new BookStatus
    //             {
    //                 ConfirmCode = "Check-out must be after check-in"
    //             };
    //         }

    //         var isBooked = await rdb.roombookings.AnyAsync(b =>
    //             b.rId == request.RoomId &&
    //             checkIn < b.CheckOut &&
    //             checkOut > b.CheckIn
    //         );

    //         if (isBooked)
    //         {
    //             return new BookStatus
    //             {
    //                 ConfirmCode = "Room already booked for selected dates"
    //             };
    //         }

    //         int tprice = (checkOut - checkIn).Days;
    //         double totalAmount = tprice * room.Price;

    //         var booking = new Data.Resortmanager.RoomBooking
    //         {
    //             CustId = request.CustId,
    //             rId = request.RoomId,
    //             CheckIn = checkIn,
    //             CheckOut = checkOut,
    //             totalPayment = totalAmount >= 0 ? totalAmount : 0
    //         };

    //         rdb.roombookings.Add(booking);
    //         await rdb.SaveChangesAsync();

    //         return new BookStatus
    //         {
    //             ConfirmCode = "Room booked successfully"
    //         };
    //     }
    //     catch (Exception)
    //     {
    //         context.Status = new Status(StatusCode.Internal, "Room booking failed due to internal exception");

    //         return new BookStatus
    //         {
    //             ConfirmCode = "Booking failed"
    //         };
    //     }
    // }

    public override async Task<BookStatus> RoomBook(BookDetail request, ServerCallContext context)
    {
        try
        {
            // ---- FIX 1: handle CustId == 0 safely ----
            int custId = request.CustId;

            if (custId == 0)
            {
                var lastCustomer = await rdb.customers
                    .OrderByDescending(c => c.CustId)
                    .FirstOrDefaultAsync();

                if (lastCustomer == null)
                {
                    return new BookStatus
                    {
                        ConfirmCode = "Customer does not exist"
                    };
                }

                custId = lastCustomer.CustId;
            }
            else
            {
                var customerExists = await rdb.customers.AnyAsync(c => c.CustId == custId);
                if (!customerExists)
                {
                    return new BookStatus
                    {
                        ConfirmCode = "Customer does not exist"
                    };
                }
            }

            // ---- FIX 2: validate room ----
            var room = await rdb.rooms.FirstOrDefaultAsync(r => r.rId == request.RoomId);

            if (room == null)
            {
                return new BookStatus
                {
                    ConfirmCode = "Room does not exist"
                };
            }

            // ---- FIX 3: dates from Timestamp ----
            var checkIn = request.CheckIn.ToDateTime();
            var checkOut = request.CheckOut.ToDateTime();

            if (checkOut <= checkIn)
            {
                return new BookStatus
                {
                    ConfirmCode = "Check-out must be after check-in"
                };
            }

            // ---- FIX 4: overlapping booking check ----
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

            // ---- FIX 5: calculate total payment ----
            int totalDays = (checkOut - checkIn).Days;
            double totalAmount = totalDays * room.Price;

            var booking = new Data.Resortmanager.RoomBooking
            {
                CustId = custId,
                rId = request.RoomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                totalPayment = totalAmount > 0 ? totalAmount : 0
            };

            rdb.roombookings.Add(booking);
            await rdb.SaveChangesAsync();

            return new BookStatus
            {
                ConfirmCode = "Room booked successfully"
            };
        }
        catch (Exception)
        {
            context.Status = new Status(
                StatusCode.Internal,
                "Room booking failed due to internal exception"
            );

            return new BookStatus
            {
                ConfirmCode = "Booking failed"
            };
        }
    }



}