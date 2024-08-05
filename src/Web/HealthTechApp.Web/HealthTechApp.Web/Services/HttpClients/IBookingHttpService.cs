
namespace HealthTechApp.Web.Services.HttpClients;

public interface IBookingHttpService
{
    Task<string> GetAnonymousPing();
    Task<string> GetAuthedPing();
}