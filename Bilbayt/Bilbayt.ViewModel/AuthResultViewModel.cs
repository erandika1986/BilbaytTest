using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.ViewModel
{
  public class AuthResultViewModel
  {
    public bool IsLoginSuccess { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public string Username { get; set; }
  }
}
