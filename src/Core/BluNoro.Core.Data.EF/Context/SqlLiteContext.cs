using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.Data.EF.Context;

public class SqlLiteContext : DbContext
{
    private string FileName { get; set; }

    public SqlLiteContext()
    {
        Database.EnsureCreated();
    }

    public SqlLiteContext(string fileName) : base()
    {
        FileName = fileName;
        Database.EnsureCreated();

        Batteries.Init();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + FileName + ".db");
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Attachment> Attachments { get; set; } = null!;
    public DbSet<SavedFile> SavedFiles { get; set; } = null!;

}
