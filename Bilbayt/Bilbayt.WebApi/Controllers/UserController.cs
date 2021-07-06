using Bilbayt.Business.Interfaces;
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

    public UserController(IUserService userService)
    {
      this.userService = userService;
    }


    [HttpGet("{id}")]
    public ActionResult Get(string id)
    {
      var response = userService.GetUserById(id);
      return Ok(response);
    }

  }
}
