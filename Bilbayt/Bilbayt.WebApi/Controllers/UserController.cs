using Bilbayt.Business.Interfaces;
using Bilbayt.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.WebApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService userService;
    private readonly IIdentityService identityService;

    public UserController(IUserService userService, IIdentityService identityService)
    {
      this.userService = userService;
      this.identityService = identityService;
    }


    [HttpGet]
    public ActionResult Get()
    {
      var userName = identityService.GetUserName();
      var response = userService.GetUserByUsername(userName);
      return Ok(response);
    }

  }
}
