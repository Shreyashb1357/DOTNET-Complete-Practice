using Grpc.Core;
using Grpc.Core.Utils;
using Sales;
using Microsoft.EntityFrameworkCore;
using data.chatting;
using Google.Protobuf.WellKnownTypes;

namespace ServerApp.Services;

public class UserServiceImplement(ChatDbContext db) : UserService.UserServiceBase
{
    //   rpc GetUserById(UserIdRequest) returns (UserMsg);
    public override async Task<UserMsg> GetUserById(UserIdRequest request, ServerCallContext context)
    {

        var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId);

        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }

        var result = new UserMsg
        {
            UserId = user.UserId,
            Username = user.Username,
            DisplayName = user.DisplayName,
            ProfileImageUrl = user.ProfileImageUrl,
            StatusMessage = user.StatusMessage,
            IsOnline = user.IsOnline,
            LastSeen = Timestamp.FromDateTime(user.LastSeen.ToUniversalTime()),
            Theme = user.Theme
        };
        return result;
    }

    //   rpc GetAllUsers(Empty) returns (GetAllUsersResponse);
   public override async Task<GetAllUsersResponse> GetAllUsers(Google.Protobuf.WellKnownTypes.Empty request , ServerCallContext Context)
    {
        var user = await db.Users.ToListAsync();
        var response = new GetAllUsersResponse();

        foreach(var entry in user)
        {
            var userMsg = new UserMsg
            {
                UserId = entry.UserId,
                Username = entry.Username,
                DisplayName = entry.DisplayName,
                ProfileImageUrl = entry.ProfileImageUrl,
                StatusMessage = entry.StatusMessage,
                IsOnline = entry.IsOnline,
                LastSeen = Timestamp.FromDateTime(entry.LastSeen.ToUniversalTime()),
                Theme = entry.Theme
            };
            response.Users.Add(userMsg);
        }
        return response;        
    }

    //   rpc SetOnlineStatus(OnlineStatusRequest) returns (OnlineStatusResponse);

    public override async Task<OnlineStatusResponse> SetOnlineStatus(OnlineStatusRequest request , ServerCallContext context)
    {
        var response = new OnlineStatusResponse();
        var user = await db.Users.FirstOrDefaultAsync(u=> u.UserId == request.UserId);
        
        if(user == null)
        {
            response.Success = false;
            return response;
        }

        user.IsOnline = request.IsOnline;

        if(!request.IsOnline)
        {
            user.LastSeen = DateTime.Now;
        }

        await db.SaveChangesAsync();

        response.Success = true;
        return response;
        
    } 

    
//   rpc SetLastSeen(LastSeenRequest) returns (LastSeenResponse);

    public override async Task<LastSeenResponse> SetLastSeen(LastSeenRequest request , ServerCallContext context)
    {
        var response = new LastSeenResponse();
        var user = await db.Users.FirstOrDefaultAsync(u=> u.UserId == request.UserId);

        if(user == null)
        {
            response.Success = false;
            return response;
        }

        user.LastSeen = request.LastSeen.ToDateTime();

        await db.SaveChangesAsync();

        response.Success = true;
        return response;       
    }

    //   rpc UpdateProfile(UpdateProfileRequest) returns (UpdateProfileResponse);

    public override async Task<UpdateProfileResponse> UpdateProfile(UpdateProfileRequest request , ServerCallContext context)
    {
        var response = new UpdateProfileResponse();
        var user = await db.Users.FirstOrDefaultAsync(a=> a.UserId == request.UserId);

        if (user == null)
        {
            return response;
        } 

        user.DisplayName = request.DisplayName;
        user.StatusMessage = request.StatusMessage;
        user.Theme = request.Theme;

        await db.SaveChangesAsync();
        response.User = new UserMsg
        {
            UserId = user.UserId,
            Username = user.Username,
            DisplayName = user.DisplayName,
            ProfileImageUrl = user.ProfileImageUrl,
            StatusMessage = user.StatusMessage,
            IsOnline = user.IsOnline,
            LastSeen = Timestamp.FromDateTime(user.LastSeen.ToUniversalTime()),
            Theme = user.Theme
        };
        return response;
    }

    //   rpc UpdateProfileImage(UpdateProfileImageRequest) returns (UpdateProfileImageResponse);
    public override async Task<UpdateProfileImageResponse> UpdateProfileImage(UpdateProfileImageRequest request, ServerCallContext context)
    {
        var response = new UpdateProfileImageResponse();

        var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId);

        if (user == null)
        {
            return response; 
        }

        user.ProfileImageUrl = request.ImageUrl;

        await db.SaveChangesAsync();

        response.User = new UserMsg
        {
            UserId = user.UserId,
            Username = user.Username,
            DisplayName = user.DisplayName,
            ProfileImageUrl = user.ProfileImageUrl,
            StatusMessage = user.StatusMessage,
            IsOnline = user.IsOnline,
            LastSeen = Timestamp.FromDateTime(user.LastSeen.ToUniversalTime()),
            Theme = user.Theme
        };

        return response;
    }

}