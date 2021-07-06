using Bilbayt.Business;
using Bilbayt.Data.Interfaces;
using Bilbayt.Model;
using Bilbayt.Model.Settings;
using Bilbayt.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bilbayt.UnitTest
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void ForSuccessLoginTestMethod()
    {
      ITokenSettings settings = new TokenSettings()
      {
        Issuer="UnitTest",
        Key= "bGyO28foMLaGBjjRHjGHYWWzb7/eEIfvjso9PQU="
      };

      var loginVm = new LoginViewModel()
      {
        Password= "Password1",
        Username= "Test1"
      };

      Mock<IUserContext> mockUserContext = new Mock<IUserContext>();

      mockUserContext.Setup(t => t.FindByUsername("Test1")).Returns(new User() { Id = "1", FullName = "Test user 1", Username = "Test1", Password = BCrypt.Net.BCrypt.HashPassword("Password1"), IsActive = true });

      var accountService = new AccountService(mockUserContext.Object, settings);

      var result = accountService.Login(loginVm);

      Assert.AreEqual(result.IsLoginSuccess, true);
      Assert.IsNotNull(result.Token);

    }

    [TestMethod]
    public void ForInvalidPasswordLoginTestMethod()
    {
      ITokenSettings settings = new TokenSettings()
      {
        Issuer = "UnitTest",
        Key = "bGyO28foMLaGBjjRHjGHYWWzb7/eEIfvjso9PQU="
      };

      var loginVm = new LoginViewModel()
      {
        Password = "Password11233",
        Username = "Test1"
      };

      Mock<IUserContext> mockUserContext = new Mock<IUserContext>();

      mockUserContext.Setup(t => t.FindByUsername("Test1")).Returns(new User() { Id = "1", FullName = "Test user 1", Username = "Test1", Password = BCrypt.Net.BCrypt.HashPassword("Password1"), IsActive = true });

      var accountService = new AccountService(mockUserContext.Object, settings);

      var result = accountService.Login(loginVm);

      Assert.AreEqual(result.IsLoginSuccess, false);
      Assert.IsNull(result.Token);
    }

    [TestMethod]
    public void ForInvalidUsernameLoginTestMethod()
    {
      ITokenSettings settings = new TokenSettings()
      {
        Issuer = "UnitTest",
        Key = "bGyO28foMLaGBjjRHjGHYWWzb7/eEIfvjso9PQU="
      };

      var loginVm = new LoginViewModel()
      {
        Password = "Password1",
        Username = "Test1111"
      };

      Mock<IUserContext> mockUserContext = new Mock<IUserContext>();

      mockUserContext.Setup(t => t.FindByUsername("Test1")).Returns(new User() { Id = "1", FullName = "Test user 1", Username = "Test1", Password = BCrypt.Net.BCrypt.HashPassword("Password1"), IsActive = true });

      var accountService = new AccountService(mockUserContext.Object, settings);

      var result = accountService.Login(loginVm);

      Assert.AreEqual(result.IsLoginSuccess, false);
      Assert.IsNull(result.Token);
    }


    [TestMethod]
    public void ForInvalidUsernamePasswordLoginTestMethod()
    {
      ITokenSettings settings = new TokenSettings()
      {
        Issuer = "UnitTest",
        Key = "bGyO28foMLaGBjjRHjGHYWWzb7/eEIfvjso9PQU="
      };

      var loginVm = new LoginViewModel()
      {
        Password = "Password1111",
        Username = "Test1111"
      };

      Mock<IUserContext> mockUserContext = new Mock<IUserContext>();

      mockUserContext.Setup(t => t.FindByUsername("Test1")).Returns(new User() { Id = "1", FullName = "Test user 1", Username = "Test1", Password = BCrypt.Net.BCrypt.HashPassword("Password1"), IsActive = true });

      var accountService = new AccountService(mockUserContext.Object, settings);

      var result = accountService.Login(loginVm);

      Assert.AreEqual(result.IsLoginSuccess, false);
      Assert.IsNull(result.Token);
    }
  }


}
