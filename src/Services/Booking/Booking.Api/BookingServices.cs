﻿namespace Booking.Api;

public class BookingServices(IMediator mediator,
    IBookingQueries queries,
    IIdentityService identityService,
    ILogger<BookingServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<BookingServices> Logger { get; } = logger;
    public IBookingQueries Queries { get; } = queries;
    public IIdentityService IdentityService { get; } = identityService;

}