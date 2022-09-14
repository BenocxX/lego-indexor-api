using Microsoft.EntityFrameworkCore;

namespace lego_indexor_api.Core.Models.Entities;

public class Database : DBContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) 
            return;
        
        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        optionsBuilder.UseNpgsql($"Host={host};Database=lego-indexor;Username=dev;Password=dev");
    }
}