namespace data.chatting;

public class GroupMessage
{
    public string MessageId{get;set;}
    public string GroupId{get;set;}
    public string SenderId{get;set;}
    public string MessageText{get;set;}
    public string MessageType { get; set; }
    public string FileUrl{get;set;}
    public DateTime SentAt{get;set;}
}