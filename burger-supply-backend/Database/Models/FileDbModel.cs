using System.ComponentModel.DataAnnotations;

namespace burger_supply_backend.Database.Models;

public class FileDbModel
{
    public int Id { get; init; }
    public required string FileName { get; init; }
    public required string FilePath { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required UserDbModel CreatedBy { get; init; }
    public required string PublicId { get; init; }
}