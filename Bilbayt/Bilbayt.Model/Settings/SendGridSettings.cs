using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Model.Settings
{
  public class SendGridSettings : ISendGridSettings
  {
    public string ApiKey { get; set; }
    public string FromUserName { get; set; }
    public string FromUserEmail { get; set; }
  }
}
