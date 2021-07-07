using Bilbayt.Business.Interfaces;
using Bilbayt.Model.Settings;
using Bilbayt.ViewModel.Common;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Business
{
  public class SendGridEmailManager : ISendGridEmailManager
  {
    private readonly ISendGridSettings sendGridSettings;
    public SendGridEmailManager(ISendGridSettings sendGridSettings)
    {
      this.sendGridSettings = sendGridSettings;
    }

    public async Task<bool> SendSingleEmail(EmailMessage email)
    {
      var client = new SendGridClient(sendGridSettings.ApiKey);

      var fromUser = new EmailAddress(sendGridSettings.FromUserEmail, sendGridSettings.FromUserName);

      var message = MailHelper.CreateSingleEmail(fromUser, email.ToUsers.FirstOrDefault(),email.Subject, email.PainTextEmailBody, email.HtmlEmailBody);

      var response = await client.SendEmailAsync(message);

      return response.IsSuccessStatusCode;
    }
  }
}
