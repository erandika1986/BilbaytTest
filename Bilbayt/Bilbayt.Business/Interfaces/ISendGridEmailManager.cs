using Bilbayt.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Business.Interfaces
{
  public interface ISendGridEmailManager
  {
    Task<bool> SendSingleEmail(EmailMessage email);
  }
}
