using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace WebObserver.Main.API.Helpers;

public static class ControllerExtensions
{
    public static int? GetUserId(this ControllerBase controller)
    {
        var claim = controller.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);
        return claim != null 
            ? int.Parse(claim.Value) 
            : null;
    }
}