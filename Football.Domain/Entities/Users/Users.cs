using Football.Domain.Entities.Commons;
using Football.Domain.Enums;

namespace Football.Domain.Entities;
public sealed class Users : AudiTable
{
    private const int DEFAULT_EXPIRE_DATE_IN_MINUTES = 48 * 60;

    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public UserRoles Role { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpireDate { get; set; }

    public Guid ClubId { get; set; }
    public Clubs? Clubs { get; set; }

    public void UpdateRefreshToken(
        string refreshToken,
        int expireDateInMinutes = DEFAULT_EXPIRE_DATE_IN_MINUTES)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpireDate = DateTime.UtcNow.AddMinutes(expireDateInMinutes);
    }
}
