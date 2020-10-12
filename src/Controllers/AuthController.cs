using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VueJwt.Models;
using VueJwt.Security;

namespace VueJwt.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    [HttpPost]
    public ActionResult<AuthResultModel> Post([FromBody] AuthRequestModel model)
    {
      // NEVER DO THIS, JUST SHOWING THE EXAMPLE
      if (model.Username == "shawn@wildermuth.com"
        && model.Password == "P@ssw0rd!")
      {
        var result = new AuthResultModel()
        {
          Success = true
        };

        // Never do this either, hardcoded strings
        var token = TokenSecurity.GenerateJwt(model.Username);
        result.Token = new JwtSecurityTokenHandler().WriteToken(token);
        result.Expiration = token.ValidTo;

        return Created("", result);

      }

      return BadRequest("Unknown failure");
    }
  }
}
