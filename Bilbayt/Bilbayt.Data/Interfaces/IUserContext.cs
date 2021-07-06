using Bilbayt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Data.Interfaces
{
  public interface IUserContext
  {
    User FindByUsername(string username);
    User FindUserById(string id);
    User Save(User user);
    Task<User> SaveAsync(User user);
  }
}
