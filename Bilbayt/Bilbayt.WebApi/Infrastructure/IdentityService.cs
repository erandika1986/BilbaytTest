using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bilbayt.WebApi.Infrastructure
{
  public class IdentityService : IIdentityService
  {
    private IHttpContextAccessor _context;

    public IdentityService(IHttpContextAccessor context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string GetUserName()
    {
      var identity = _context.HttpContext.User.Identity as ClaimsIdentity;
      var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
      var username = claim != null ? claim.Value : string.Empty;

      return username;
    }
  }
}
