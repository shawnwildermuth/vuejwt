using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace VueJwt.Security
{
  public static class TokenSecurity
  {
    // Hard coded security keys
    // DO NOT DO THIS, SERIOUSLY
    // JUST AN EXAMPLE
    public static readonly string SIGNING_KEY = "FOOBARQUUX_FOOBARQUUX_FOOBARQUUX_FOOBARQUUX";
    public static readonly string ISSUER = "localhost";
    public static readonly string AUDIENCE = "All";

    public static JwtSecurityToken GenerateJwt(string email)
    {
      var claims = new List<Claim>()
      {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, email)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SIGNING_KEY));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

      var token = new JwtSecurityToken(
        ISSUER,
        AUDIENCE,
        claims,
        expires: DateTime.Now.AddMinutes(20),
        signingCredentials: creds);

      return token;

    }

  }
}
