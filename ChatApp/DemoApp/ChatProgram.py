import grpc
from google.protobuf.empty_pb2 import Empty
from google.protobuf.timestamp_pb2 import Timestamp

import contract_pb2 as pb
import contract_pb2_grpc as rpc


def print_header(title):
    print("\n" + "=" * 40)
    print(title)
    print("=" * 40)


def main():

    # CONNECT TO gRPC SERVER
    channel = grpc.insecure_channel("localhost:5090")

    users = rpc.UserServiceStub(channel)
    chat = rpc.ChatServiceStub(channel)

    # USER IDs IN DATABASE:
    # 1 → user1
    # 2 → user2

    USER1 = "1"
    USER2 = "2"

    # ------------------------------------
    print_header("TEST: GetAllUsers")
    # ------------------------------------
    try:
        reply = users.GetAllUsers(Empty())
        print(reply)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: GetUserById")
    # ------------------------------------
    try:
        user = users.GetUserById(pb.UserIdRequest(userId=USER1))
        print(user)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: SetOnlineStatus")
    # ------------------------------------
    try:
        reply = users.SetOnlineStatus(
            pb.OnlineStatusRequest(userId=USER1, isOnline=True)
        )
        print(reply)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: SetLastSeen")
    # ------------------------------------
    try:
        ts = Timestamp()
        ts.GetCurrentTime()
        reply = users.SetLastSeen(
            pb.LastSeenRequest(userId=USER1, lastSeen=ts)
        )
        print(reply)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: UpdateProfile")
    # ------------------------------------
    try:
        reply = users.UpdateProfile(
            pb.UpdateProfileRequest(
                userId=USER1,
                displayName="Test User",
                statusMessage="Hello World!",
                theme="dark"
            )
        )
        print(reply)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: UpdateProfileImage")
    # ------------------------------------
    try:
        reply = users.UpdateProfileImage(
            pb.UpdateProfileImageRequest(
                userId=USER1,
                imageUrl="https://example.com/photo.png"
            )
        )
        print(reply)
    except grpc.RpcError as e:
        print("Error:", e.details())


    # ==================================================
    #              CHAT SERVICE TESTS
    # ==================================================

    # ------------------------------------
    print_header("TEST: CreateChatRoom")
    # ------------------------------------
    try:
        room = chat.CreateChatRoom(
            pb.ChatRoomRequest(user1Id=USER1, user2Id=USER2)
        )
        print(room)
    except grpc.RpcError as e:
        print("Error:", e.details())
        return

    # ------------------------------------
    print_header("TEST: SendMessage")
    # ------------------------------------
    try:
        msg = chat.SendMessage(
            pb.SendMessageRequest(
                chatRoomId=room.chatRoom.chatRoomId,
                senderId=USER1,
                messageText="Hello from Python!",
                fileUrl=""
            )
        )
        print(msg)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: GetMessages")
    # ------------------------------------
    try:
        msgs = chat.GetMessages(
            pb.GetMessagesRequest(chatRoomId=room.chatRoom.chatRoomId)
        )
        print(msgs)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: MarkAsRead")
    # ------------------------------------
    try:
        mark = chat.MarkAsRead(
            pb.MarkAsReadRequest(messageIds=[msg.message.messageId])
        )
        print(mark)
    except grpc.RpcError as e:
        print("Error:", e.details())


    # ------------------------------------
    print_header("TEST: CreateGroup")
    # ------------------------------------
    try:
        group = chat.CreateGroup(
            pb.CreateGroupRequest(
                name="MyGroup",
                userIds=[USER1, USER2]    # MUST EXIST IN Users TABLE
            )
        )
        print(group)
    except grpc.RpcError as e:
        print("Error:", e.details())
        return

    # ------------------------------------
    print_header("TEST: SendGroupMessage")
    # ------------------------------------
    try:
        gmsg = chat.SendGroupMessage(
            pb.SendGroupMessageRequest(
                groupId=group.groupId,
                senderId=USER1,
                messageText="Hello Group!",
                fileUrl=""
            )
        )
        print(gmsg)
    except grpc.RpcError as e:
        print("Error:", e.details())

    # ------------------------------------
    print_header("TEST: AddGroupMember")
    # ------------------------------------
    try:
        # ONLY add existing users (1 or 2)
        add = chat.AddGroupMember(
            pb.AddGroupMemberRequest(
                groupId=group.groupId,
                userId=USER2
            )
        )
        print(add)
    except grpc.RpcError as e:
        print("Error:", e.details())


if __name__ == "__main__":
    main()
