using Booking.Application.Commands;
using Booking.Application.Queries.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public static class BookingAPI
{
    public static RouteGroupBuilder MapBookingsApi(this RouteGroupBuilder group)
    {
        group.RequireAuthorization();

        group.MapGet("/pinganonymous", () => "pong").AllowAnonymous();
        group.MapGet("/pingauthed", () => "authed pong");


        group.MapGet("/", GetAppointments);
        group.MapGet("/{appointmentId}", GetAppointment);

        group.MapPost("/", CreateAppointmentAsync).AllowAnonymous();
        group.MapPost("/approve/{appointmentId}", ApproveAppointmentAsync);
        group.MapPut("/", UpdateAppointmentAsync);
        group.MapDelete("/{appointmentId}", DeleteAppointmentAsync);


        return group;
    }
    public static async Task<Results<Ok<Appointment>, NotFound<int>>> GetAppointment(int appointmentId, [AsParameters] BookingServices services)
    {
        var collection = await services.Queries.GetAppointment(appointmentId);
        if (collection is null)
        {
            return TypedResults.NotFound(appointmentId);
        }
        return TypedResults.Ok(collection);
    }

    public static async Task<Ok<IEnumerable<AppointmentSummary>>> GetAppointments([AsParameters] BookingServices services)
    {
        var userId = services.IdentityService.GetUserIdentity();
        var results = await services.Queries.GetAppointments();
        return TypedResults.Ok(results);
    }

    public static async Task<Ok> CreateAppointmentAsync([FromBody] CreateBookingRequest request, [AsParameters] BookingServices services)
    {
        var command = new CreateAppointmentCommand(request.AppointmentDate, request.Issue, request.Email, request.Name, request.ContactNumber);
        await services.Mediator.Send(command);
        return TypedResults.Ok();
    }
    public static async Task<Results<BadRequest, Ok>> ApproveAppointmentAsync(int appointmentId, [AsParameters] BookingServices services)
    {
        var command = new ApproveAppointmentCommand(appointmentId);
        bool commandResult = await services.Mediator.Send(command);
        if (!commandResult)
        {
            return TypedResults.BadRequest();
        }
        return TypedResults.Ok();
    }
    public static async Task<Results<BadRequest, Ok>> UpdateAppointmentAsync([FromBody] UpdateAppointmentCommand request, [AsParameters] BookingServices services)
    {
        bool commandResult = await services.Mediator.Send(request);
        if (!commandResult)
        {
            return TypedResults.BadRequest();
        }
        return TypedResults.Ok();
    }


    public static async Task<Results<BadRequest, Ok>> DeleteAppointmentAsync(int appointmentId, [AsParameters] BookingServices services)
    {
        var command = new DeleteAppointmentCommand(appointmentId);
        bool commandResult = await services.Mediator.Send(command);
        if (!commandResult)
        {
            return TypedResults.BadRequest();
        }
        return TypedResults.Ok();
    }
}

public record CreateBookingRequest(DateTime AppointmentDate, string Issue, string Email, string Name, string ContactNumber);
