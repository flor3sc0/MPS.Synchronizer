using System.IdentityModel.Tokens.Jwt;
using MPS.Synchronizer.Application.CommonModels;

namespace MPS.Synchronizer.Application.Extensions;

public static class JwtExtensions
{
    public static JwtToken ParseAsJwt(this string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var claims = jwtSecurityToken.Claims.ToList();
        return new JwtToken
        {
            Id = Guid.Parse(claims.First(claim => claim.Type == "id").Value),
            S = uint.Parse(claims.First(claim => claim.Type == "s").Value),
            Sid = Guid.Parse(claims.First(claim => claim.Type == "sid").Value),
            Exp = jwtSecurityToken.ValidTo,
            T = bool.Parse(claims.First(claim => claim.Type == "t").Value)
        };
    }
}