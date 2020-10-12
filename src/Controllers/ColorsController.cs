using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VueJwt.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ColorsController
  {
    [HttpGet]
    public ActionResult<string[]> Get()
    {
      return new[] { "Blue", "Red", "Green" };
    }
  }
}
