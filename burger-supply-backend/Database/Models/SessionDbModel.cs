namespace burger_supply_backend.Database.Models;

public class SessionDbModel
{
    public int Id { get; init; }
    public required string SessionId { get; init; }
    public required UserDbModel User { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime ExpiresAt { get; init; }
}