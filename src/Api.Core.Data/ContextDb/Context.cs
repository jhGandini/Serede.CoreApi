﻿using Api.Core.Models.Interfaces;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Core.Data.ContextDb;

public abstract class Context : DbContext, IUnitOfWork
{
    private readonly ILoggerFactory _loggerFactory;

    protected Context(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder opt)
    {
        opt.UseLoggerFactory(_loggerFactory)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.Ignore<Notification>();
    }

    public bool Commit()
    {
        try
        {
            return base.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }

    }

    public async Task<bool> CommitAsync()
    {
        try
        {
            return await base.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }
}
