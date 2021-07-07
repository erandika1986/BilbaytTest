using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Model.Settings
{
  public interface ISendGridSettings
  {
    string ApiKey { get; set; }
    string FromUserName { get; set; }
    string FromUserEmail { get; set; }
  }
}
