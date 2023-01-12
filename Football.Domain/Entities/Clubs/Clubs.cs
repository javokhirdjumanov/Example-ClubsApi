namespace Football.Domain.Entities;
public sealed class Clubs
{
    public Guid Id { get; set; }
    public string ClubName { get; set; }
    public string ClubCountry { get; set; }
    public string? ClubCity { get; set; }

    public ICollection<Users> Users { get; set; }
}
