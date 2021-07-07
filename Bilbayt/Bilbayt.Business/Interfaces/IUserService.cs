using Bilbayt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Business.Interfaces
{
  public interface IUserService
  {
    UserViewModel GetUserByUsername(string userName);
  }
}
