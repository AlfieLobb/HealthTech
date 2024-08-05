using HealthTechApp.Web.Services.Models.Booking;

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


    public async Task<Appointment?> GetAppointmentAsync(int appointmentId)
    {
        var uri = $"{_baseUrl}{appointmentId}";
        logger.LogDebug($"[{nameof(GetAppointmentAsync)}] -> Calling {uri} to get the appointment");
        var response = await httpClient.GetAsync(uri);
        logger.LogDebug($"[{nameof(GetAppointmentAsync)}] -> response code {response.StatusCode}");
        if (response.IsSuccessStatusCode)
        {
            var value = await response.Content.ReadFromJsonAsync<Appointment>();
            return value;
        }
        return null;
    }

    public async Task<IEnumerable<AppointmentSummary>> GetAppointmentsAsync()
    {
        var uri = $"{_baseUrl}";
        logger.LogDebug($"[{nameof(GetAppointmentAsync)}] -> Calling {uri} to get the appointments");
        var response = await httpClient.GetAsync(uri);
        logger.LogDebug($"[{nameof(GetAppointmentAsync)}] -> response code {response.StatusCode}");
        if (response.IsSuccessStatusCode)
        {
            var value = await response.Content.ReadFromJsonAsync<AppointmentSummary[]>();
            return value;
        }
        return null;
    }

    public async Task CreateAppointmentAsync(CreateAppointmentRequest data)
    {
        logger.LogDebug($"[{nameof(CreateAppointmentAsync)}] -> Calling to {_baseUrl} to add the appointment");
        var response = await httpClient.PostAsJsonAsync(_baseUrl, data);
        logger.LogDebug($"[{nameof(CreateAppointmentAsync)}] -> response code {response.StatusCode}");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateCollectionAsync(UpdateAppointmentRequest data)
    {
        logger.LogDebug($"[{nameof(UpdateCollectionAsync)}] -> Calling to {_baseUrl} to update the appointment {data.AppointmentId}");
        var response = await httpClient.PutAsJsonAsync(_baseUrl, data);
        logger.LogDebug($"[{nameof(UpdateCollectionAsync)}] -> response code {response.StatusCode}");
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAppointmentAsync(int appointmentId)
    {
        var uri = $"{_baseUrl}{appointmentId}";
        logger.LogDebug($"[{nameof(DeleteAppointmentAsync)}] -> Calling to {uri} to delete the appointment");
        var response = await httpClient.DeleteAsync(uri);
        logger.LogDebug($"[{nameof(DeleteAppointmentAsync)}] -> response code {response.StatusCode}");
        response.EnsureSuccessStatusCode();
        return;
    }

    public async Task ApproveAppointmentAsync(int appointmentId)
    {
        var uri = $"{_baseUrl}approve/{appointmentId}";
        logger.LogDebug($"[{nameof(ApproveAppointmentAsync)}] -> Calling to {uri} to approve the appointment");
        var response = await httpClient.PostAsync(uri, new StringContent(string.Empty));
        logger.LogDebug($"[{nameof(ApproveAppointmentAsync)}] -> response code {response.StatusCode}");
        response.EnsureSuccessStatusCode();
        return;
    }
}
