using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueJwt.Models
{
  public class AuthResultModel
  {
    public bool Success { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
  }
}
