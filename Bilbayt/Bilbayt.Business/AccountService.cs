using Bilbayt.Business.Interfaces;
using Bilbayt.Data.Interfaces;
using Bilbayt.Model;
using Bilbayt.Model.Settings;
using Bilbayt.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Bilbayt.Business
{
  public class AccountService : IAccountService
  {
    private readonly IUserContext userContext;
    private readonly ITokenSettings settings;

    public AccountService(IUserContext userContext, ITokenSettings settings)
    {
      this.userContext = userContext;
      this.settings = settings;
    }


    public AuthResultViewModel Login(LoginViewModel model)
    {
      var response = new AuthResultViewModel();

      var user = userContext.FindByUsername(model.Username.Trim());

      if (user == null)
      {
        response.IsLoginSuccess = false;
        response.Message = "Login failed.Invalid username.";

        return response;
      }

      if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
      {
        response.IsLoginSuccess = false;
        response.Message = "Login failed.Invalid password.";

        return response;
      }

      response.IsLoginSuccess = true;
      response.Message = "User has authenticated successfully";
      response.Token = generateJwtToken(user);
      user.Username = user.Username;

      return response;

    }

    public async Task<ResponseViewModel> RegisterAsync(RegisterRequestViewModel model)
    {
      var response = new ResponseViewModel();

      try
      {
        var user = userContext.FindByUsername(model.Username);
        if(user!=null)
        {
          response.IsSuccess = false;
          response.Message = "Username is already taken";

          return response;
        }

        user = new User()
        {
          CreatedOn = DateTime.UtcNow,
          FullName = model.FullName,
          IsActive = true,
          UpdatedOn = DateTime.UtcNow,
          Username = model.Username.Trim(),
          Password = BCrypt.Net.BCrypt.HashPassword(model.Password.Trim())
        };

        user = await userContext.SaveAsync(user);

        response.IsSuccess = true;
        response.Message = "User has registered successfully.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Registration failed. Please try again.";
      }

      return response;
    }


    private string generateJwtToken(User user)
    {
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
      var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

      var now = DateTime.UtcNow;
      DateTime nowDate = DateTime.UtcNow;
      var claims = new[]
      {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Aud,"angularApp"),
                        new Claim(ClaimTypes.Role,"User")
                    };

      var tokeOptions = new JwtSecurityToken(
          issuer: settings.Issuer,
          claims: claims,
          expires: DateTime.Now.AddMinutes(60),
          signingCredentials: signinCredentials
      );
      var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

      return tokenString;
    }
  }
}
