
namespace Booking.Application.Services.Identity;
public class IdentityService(IHttpContextAccessor contextAccessor) : IIdentityService
{
    public string GetUserIdentity()
    {
        return contextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
