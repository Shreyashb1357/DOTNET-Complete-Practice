namespace data.chatting;

public class Message
{
    public string MessageId{get;set;}
    public string ChatRoomId{get;set;}
    public string SenderId{get;set;}
    public string? MessageText{get;set;}
    public string MessageType{get;set;}
    public string FileUrl{get;set;}
    public DateTime SentAt {get;set;}
    public bool IsRead{get;set;}
    public DateTime? ReadAt{get;set;}
}