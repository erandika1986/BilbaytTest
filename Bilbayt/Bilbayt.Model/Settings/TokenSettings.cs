using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Model.Settings
{
  public class TokenSettings: ITokenSettings
  {
    public string Key { get; set; }
    public string Issuer { get; set; }
  }
}
