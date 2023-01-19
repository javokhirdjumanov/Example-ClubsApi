namespace Football.Infrastructure.Authentication;
public interface IPasswordHasher
{
    public string Encrypt(string username, string salt);
    public bool Verify(string hash, string password, string salt);
}
