namespace data.chatting;

public class GroupMember
{
    public int Id{get;set;}
    public string GroupId { get; set; }
    public string UserId { get; set; }
    public DateTime JoinedAt{get;set;}
}