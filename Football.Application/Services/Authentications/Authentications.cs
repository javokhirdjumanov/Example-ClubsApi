using Football.Application.DataTransferObjects.Authentication;
using Football.Infrastructure.Repositories.UserRepositories;
using Football.Infrastructure.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Football.Domain.Exceptions;
using System.Security.Claims;
using System.Text;

namespace Football.Application.Services.Authentications;
public partial class Authentications : IAuthentications
{
    /// <summary>
    /// C O N S T R U C T O R
    /// </summary>
    private readonly IUserRepositiory userRepositiory;
    private readonly IJwtTokenHandler jwtTokenHandler;
    private readonly IPasswordHasher passwordHasher;
    private readonly Jwtoption jwtoption;
    public Authentications(IUserRepositiory userRepositiory,IJwtTokenHandler jwtTokenHandler, 
                           IPasswordHasher passwordHasher, IOptions<Jwtoption> option)
    {
        this.userRepositiory = userRepositiory;
        this.jwtTokenHandler = jwtTokenHandler;
        this.passwordHasher = passwordHasher;
        this.jwtoption = option.Value;
    }

    /// <summary>
    /// A C C E S S  token
    /// </summary>
    public async Task<TokenDto> AccessTokenAsync(AuthenticationsDto authenticationsDto)
    {
        var storegeUser = await this.userRepositiory.SelectByIdWithDetaialsAsync(
            expression: user => user.Email == authenticationsDto.email,
            includes: Array.Empty<string>()
        );

        ValidationForStorageUser(user: storegeUser, userId: storegeUser.Id);

        if (!this.passwordHasher.Verify(
            hash: storegeUser.PasswordHash,
            password: authenticationsDto.password,
            salt: storegeUser.Salt))
        {
            throw new ValidationExceptions("Username or password is not in valid :(");
        }

        string refreshToken = this.jwtTokenHandler.GenerateRefreshToken();

        storegeUser.UpdateRefreshToken(refreshToken);

        var updateUser = await this.userRepositiory.UpdateAsync(storegeUser);

        var token = this.jwtTokenHandler.GenerationAccessToken(storegeUser);

        var accessToken = this.jwtTokenHandler.GenerationAccessToken(updateUser);

        return new TokenDto(
            accessToken: new JwtSecurityTokenHandler().WriteToken(token),
            refleshToken: null,
            expireDate: token.ValidTo
            );
    }

    /// <summary>
    /// R E F R E S H  token
    /// </summary>
    public async Task<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var principal = GetPricipalFromExpiredToken(refreshTokenDto.accessToken);

        var userId = principal.FindFirstValue(CustomClaimNames.Id);

        var storageUser = await this.userRepositiory.SelectByIdAsync(Guid.Parse(userId));

        ValidationEqualRefreshToken(storageUser: storageUser, refreshTokenDto: refreshTokenDto);

        ValidationExpireDateForRefreshToken(storageUser: storageUser);

        var newAccessToken = this.jwtTokenHandler.GenerationAccessToken(storageUser);

        return new TokenDto(
            accessToken: new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refleshToken: storageUser.RefreshToken,
            expireDate: newAccessToken.ValidTo);
    }
    
    /// <summary>
    /// Get Claim Pricipal
    /// </summary>
    private ClaimsPrincipal GetPricipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParametr = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = this.jwtoption.Audience,
            ValidateIssuer = true,
            ValidIssuer = this.jwtoption.Issuer,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtoption.SecretKey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(
            token: accessToken,
            validationParameters: tokenValidationParametr,
            validatedToken: out SecurityToken securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(
            SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ValidationExceptions("Invalid token");
        }

        return principal;
    }
}
