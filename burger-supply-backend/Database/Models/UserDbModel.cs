namespace burger_supply_backend.Database.Models;

public class UserDbModel
{
    public int Id { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}