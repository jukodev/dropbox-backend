using burger_supply_backend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace burger_supply_backend.Database;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<FileDbModel> Files { get; set; }
}