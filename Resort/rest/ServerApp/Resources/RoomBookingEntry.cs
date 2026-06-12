namespace ServerApp.Resources;

public readonly record struct RoomBookingEntry (
    int BookingId , int CustomerId , int RoomId , DateTime CheckIn , DateTime CheckOut , double? TotalPayment
);