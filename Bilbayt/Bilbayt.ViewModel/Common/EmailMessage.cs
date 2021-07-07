using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.ViewModel.Common
{
  public class EmailMessage
  {
    public EmailMessage()
    {
      ToUsers = new List<EmailAddress>();
    }

    public EmailAddress FromUser { get; set; }
    public List<EmailAddress> ToUsers { get; set; }
    public string Subject { get; set; }
    public string PainTextEmailBody { get; set; }
    public string HtmlEmailBody { get; set; }

  }
}
