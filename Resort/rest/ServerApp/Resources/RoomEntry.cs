namespace ServerApp.Resources;

public readonly record struct RoomEntry (
    int RoomId , int RoomNumber , string RoomType , double Price, int DeptId
);