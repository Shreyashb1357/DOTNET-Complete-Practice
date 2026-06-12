using Grpc.Core;
using Grpc.Core.Utils;
using Sales;
using Microsoft.EntityFrameworkCore;
using data.chatting;
using Google.Protobuf.WellKnownTypes;

namespace ServerApp.Services;

public class ChatServiceImplement(ChatDbContext db) : ChatService.ChatServiceBase
{
    //rpc CreateChatRoom(ChatRoomRequest) returns (ChatRoomResponse);
    public override async Task<ChatRoomResponse> CreateChatRoom(ChatRoomRequest request, ServerCallContext context)
    {
        var response = new ChatRoomResponse();

        var existingRoom = await db.ChatRooms
            .FirstOrDefaultAsync(r =>
                (r.User1Id == request.User1Id && r.User2Id == request.User2Id) ||
                (r.User1Id == request.User2Id && r.User2Id == request.User1Id));

        if (existingRoom != null)
        {
            response.ChatRoom = new ChatRoomMsg
            {
                ChatRoomId = existingRoom.ChatRoomId,
                User1Id = existingRoom.User1Id,
                User2Id = existingRoom.User2Id,
                CreatedAt = Timestamp.FromDateTime(existingRoom.CreatedAt.ToUniversalTime())
            };
            return response;
        }

        var newRoom = new ChatRoom
        {
            ChatRoomId = Guid.NewGuid().ToString(),
            User1Id = request.User1Id,
            User2Id = request.User2Id,
            CreatedAt = DateTime.Now
        };

        db.ChatRooms.Add(newRoom);
        await db.SaveChangesAsync();

        response.ChatRoom = new ChatRoomMsg
        {
            ChatRoomId = newRoom.ChatRoomId,
            User1Id = newRoom.User1Id,
            User2Id = newRoom.User2Id,
            CreatedAt = Timestamp.FromDateTime(newRoom.CreatedAt.ToUniversalTime())
        };

        return response;
    }

    // rpc SendMessage(SendMessageRequest) returns (SendMessageResponse);
    public override async Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        var response = new SendMessageResponse();

        var msg = new Message
        {
            MessageId = Guid.NewGuid().ToString(),
            ChatRoomId = request.ChatRoomId,
            SenderId = request.SenderId,
            MessageText = request.MessageText,
            FileUrl = request.FileUrl,
            MessageType = string.IsNullOrWhiteSpace(request.FileUrl) ? "text" : "file",
            SentAt = DateTime.Now,
            IsRead = false
        };

        db.Messages.Add(msg);
        await db.SaveChangesAsync();

        response.Message = new MessageMsg
        {
            MessageId = msg.MessageId,
            ChatRoomId = msg.ChatRoomId,
            SenderId = msg.SenderId,
            MessageText = msg.MessageText ?? "",
            MessageType = msg.MessageType,
            FileUrl = msg.FileUrl ?? "",
            SentAt = Timestamp.FromDateTime(msg.SentAt.ToUniversalTime()),
            IsRead = msg.IsRead,
            ReadAt = null
        };

        return response;
    }

   
    // -  rpc GetMessages(GetMessagesRequest) returns (GetMessagesResponse);
    public override async Task<GetMessagesResponse> GetMessages(GetMessagesRequest request, ServerCallContext context)
    {
        var response = new GetMessagesResponse();

        var messages = await db.Messages
            .Where(m => m.ChatRoomId == request.ChatRoomId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();

        foreach (var m in messages)
        {
            response.Messages.Add(new MessageMsg
            {
                MessageId = m.MessageId,
                ChatRoomId = m.ChatRoomId,
                SenderId = m.SenderId,
                MessageText = m.MessageText ?? "",
                MessageType = m.MessageType,
                FileUrl = m.FileUrl ?? "",
                SentAt = Timestamp.FromDateTime(m.SentAt.ToUniversalTime()),
                IsRead = m.IsRead,
                ReadAt = m.ReadAt == null ? null : Timestamp.FromDateTime(m.ReadAt.Value.ToUniversalTime())
            });
        }

        return response;
    }

    // rpc MarkAsRead(MarkAsReadRequest) returns (MarkAsReadResponse);
    public override async Task<MarkAsReadResponse> MarkAsRead(MarkAsReadRequest request, ServerCallContext context)
    {
        var response = new MarkAsReadResponse();

        var messages = await db.Messages
            .Where(m => request.MessageIds.Contains(m.MessageId))
            .ToListAsync();

        foreach (var msg in messages)
        {
            msg.IsRead = true;
            msg.ReadAt = DateTime.Now;
        }

        await db.SaveChangesAsync();

        response.Success = true;
        return response;
    }

    // rpc CreateGroup(CreateGroupRequest) returns (CreateGroupResponse);
    public override async Task<CreateGroupResponse> CreateGroup(CreateGroupRequest request, ServerCallContext context)
    {
        var group = new ChatGroup
        {
            GroupId = Guid.NewGuid().ToString(),
            Name = request.Name,
            CreatedByUserId = request.UserIds.First(),
            CreatedAt = DateTime.Now
        };

        db.ChatGroups.Add(group);
        await db.SaveChangesAsync();

        foreach (var uid in request.UserIds)
        {
            db.GroupMembers.Add(new GroupMember
            {
                GroupId = group.GroupId,
                UserId = uid,
                JoinedAt = DateTime.Now
            });
        }

        await db.SaveChangesAsync();

        return new CreateGroupResponse
        {
            GroupId = group.GroupId
        };
    }

    //  rpc SendGroupMessage(SendGroupMessageRequest) returns (SendGroupMessageResponse);
    public override async Task<AddGroupMemberResponse> AddGroupMember(AddGroupMemberRequest request, ServerCallContext context)
    {
        var response = new AddGroupMemberResponse();

        var exists = await db.GroupMembers.AnyAsync(g =>
            g.GroupId == request.GroupId && g.UserId == request.UserId);

        if (exists)
        {
            response.Success = false;
            return response;
        }

        db.GroupMembers.Add(new GroupMember
        {
            GroupId = request.GroupId,
            UserId = request.UserId,
            JoinedAt = DateTime.Now
        });

        await db.SaveChangesAsync();

        response.Success = true;
        return response;
    }

    //   rpc AddGroupMember(AddGroupMemberRequest) returns (AddGroupMemberResponse);
    public override async Task<SendGroupMessageResponse> SendGroupMessage(SendGroupMessageRequest request, ServerCallContext context)
    {
        var response = new SendGroupMessageResponse();

        var msg = new GroupMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            GroupId = request.GroupId,
            SenderId = request.SenderId,
            MessageText = request.MessageText,
            FileUrl = request.FileUrl,
            SentAt = DateTime.Now
        };

        db.GroupMessages.Add(msg);
        await db.SaveChangesAsync();

        response.Message = new GroupMessageMsg
        {
            MessageId = msg.MessageId,
            GroupId = msg.GroupId,
            SenderId = msg.SenderId,
            MessageText = msg.MessageText ?? "",
            MessageType = string.IsNullOrWhiteSpace(msg.FileUrl) ? "text" : "file",
            FileUrl = msg.FileUrl ?? "",
            SentAt = Timestamp.FromDateTime(msg.SentAt.ToUniversalTime())
        };

        return response;
    }

}