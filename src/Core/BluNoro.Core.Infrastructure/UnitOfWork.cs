using BluNoro.Core.Common.Entities;
using BluNoro.Core.Data.EF.Context;
using BluNoro.Core.Data.EF.Repositories;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Infrastructure.Logger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BluNoro.Core.Infrastructure;

public class UnitOfWork
{
    private readonly SqlLiteContext _context;

    public UserRepository Users { get; }
    public ChatRepository Chats { get; }
    public MessageRepository Messages { get; }
    public AttachmentRepository Attachments { get; }
    public SavedFileRepository SavedFiles { get; }

    public ILogger Logger { get; set; }

    public UnitOfWork(SqlLiteContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Chats = new ChatRepository(_context);
        Messages = new MessageRepository(_context);
        Attachments = new AttachmentRepository(_context);
        SavedFiles = new SavedFileRepository(_context);
    }

    public void Save()
    {
        var changes = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .ToList();

        foreach (var entityEntry in changes)
        {
            Logger.Add(LogFactory.ContextChange(entityEntry));
        }

        _context.SaveChanges();
    }
}