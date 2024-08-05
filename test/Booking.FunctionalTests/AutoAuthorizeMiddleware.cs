using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Booking.FunctionalTests;

class AutoAuthorizeMiddleware
{
    public const string IDENTITY_ID = "d453c4a7-09e2-4c81-accb-95cb818c1357";

    private readonly RequestDelegate _next;

    public AutoAuthorizeMiddleware(RequestDelegate rd)
    {
        _next = rd;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var identity = new ClaimsIdentity("cookies");

        identity.AddClaim(new Claim("sub", IDENTITY_ID));
        identity.AddClaim(new Claim("unique_name", IDENTITY_ID));
        identity.AddClaim(new Claim(ClaimTypes.Name, IDENTITY_ID));

        httpContext.User.AddIdentity(identity);

        await _next.Invoke(httpContext);
    }
}