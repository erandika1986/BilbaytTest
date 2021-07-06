using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Model.Settings
{
  public interface ITokenSettings
  {
    string Key { get; set; }
    string Issuer { get; set; }
  }
}
