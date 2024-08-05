namespace HealthTechApp.Web.Services.HttpClients;


public class BookingHttpService(HttpClient httpClient, ILogger<BookingHttpService> logger) : IBookingHttpService
{
    private readonly string _baseUrl = "api/bookings/";

    public async Task<string> GetAuthedPing()
    {
        var uri = $"{_baseUrl}pingauthed";

        logger.LogDebug($"[{nameof(GetAuthedPing)}] -> Calling {uri} to get the Authed response");
        try
        {
            var response = await httpClient.GetAsync(uri);

            logger.LogDebug($"[{nameof(GetAuthedPing)}] -> response code {response.StatusCode}");
            var value = await response.Content.ReadAsStringAsync() ?? string.Empty;
            return value;
        }
        catch (Exception e)
        {

            return e.Message;
        }
    }

    public async Task<string> GetAnonymousPing()
    {
        var uri = $"{_baseUrl}pinganonymous";

        logger.LogDebug($"[{nameof(GetAnonymousPing)}] -> Calling {uri} to get the Anonymous response");
        var response = await httpClient.GetAsync(uri);
        logger.LogDebug($"[{nameof(GetAnonymousPing)}] -> response code {response.StatusCode}");
        var value = await response.Content.ReadAsStringAsync() ?? string.Empty;
        return value;
    }
}
