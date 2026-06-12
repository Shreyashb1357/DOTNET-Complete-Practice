using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
namespace data.chatting;

public class ChatDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<ChatGroup> ChatGroups { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
    public DbSet<GroupMessage> GroupMessages { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u=> u.UserId);
        
        modelBuilder.Entity<GroupMessage>()
            .ToTable("GroupMessages")
            .HasKey(u=> u.MessageId);

        modelBuilder.Entity<ChatGroup>()
            .ToTable("ChatGroups")
            .HasKey(u => u.GroupId);

        modelBuilder.Entity<Message>()
            .ToTable("Messages")
            .HasKey(u => u.MessageId);

        modelBuilder.Entity<GroupMember>()
            .ToTable("GroupMembers")
            .HasKey(u => u.Id);

        modelBuilder.Entity<ChatRoom>()
            .ToTable("ChatRooms")
            .HasKey(u=> u.ChatRoomId);
        
    }
}
