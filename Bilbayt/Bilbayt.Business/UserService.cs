using Bilbayt.Business.Interfaces;
using Bilbayt.Data.Interfaces;
using Bilbayt.Model;
using Bilbayt.Model.Settings;
using Bilbayt.ViewModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Business
{
  public class UserService : IUserService
  {
    private readonly IUserContext userContext;

    public UserService(IUserContext userContext)
    {
      this.userContext = userContext;
    }

    public UserViewModel GetUserByUsername(string userName)
    {
      var user = userContext.FindByUsername(userName);
      var response = new UserViewModel()
      {
        Id = user.Id,
        FullName = user.FullName,
        Username = user.Username
      };

      return response;
    }
  }
}
