public static class BookingAPI
{
    public static RouteGroupBuilder MapBookingsApi(this RouteGroupBuilder group)
    {
        //group.RequireAuthorization();
        group.MapGet("/pinganonymous", () => "pong").AllowAnonymous();
        group.MapGet("/pingauthed", () => "authed pong").RequireAuthorization();
        return group;
    }
}