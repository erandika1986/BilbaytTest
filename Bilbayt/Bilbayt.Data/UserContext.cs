using Bilbayt.Data.Interfaces;
using Bilbayt.Model;
using Bilbayt.Model.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Data
{
  public class UserContext:IUserContext
  {
    private readonly IMongoCollection<User> users;

    public UserContext(IBilbaytDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);
     
      users = database.GetCollection<User>(settings.UsersCollectionName);

    }

    public User FindByUsername(string username)
    {
      var user = users.Find<User>(u => u.Username.ToLower() == username.ToLower()).FirstOrDefault();
      return user;
    }

    public User FindUserById(string id)
    {
      var user = users.Find<User>(book => book.Id == id).FirstOrDefault();
      return user;
    }
    public  User Save(User user)
    {
      users.InsertOne(user);
      return user;
    }

    public async Task<User> SaveAsync(User user)
    {
      await users.InsertOneAsync(user);
      return user;
    }
  }
}
