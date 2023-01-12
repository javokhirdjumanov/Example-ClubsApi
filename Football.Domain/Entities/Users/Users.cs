using Football.Domain.Entities.Commons;
using Football.Domain.Enums;

namespace Football.Domain.Entities;
public sealed class Users : AudiTable
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public UserRoles Role { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    public Guid ClubId { get; set; }
    public Clubs? Clubs { get; set; }
}
