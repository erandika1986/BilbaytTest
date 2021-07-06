using Bilbayt.Business.Interfaces;
using Bilbayt.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly IAccountService accountService;
    public AccountController(IAccountService accountService)
    {
      this.accountService = accountService;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginViewModel user)
    {
      if (user == null)
      {
        return BadRequest("Invalid client request");
      }

      var loginResponse = accountService.Login(user);

      return Ok(loginResponse);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestViewModel request)
    {
      if (request == null)
      {
        return BadRequest("Invalid request");
      }

      var registrationResponse = await accountService.RegisterAsync(request);

      return Ok(registrationResponse);
    }
  }
}
