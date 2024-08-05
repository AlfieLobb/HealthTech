using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Exceptions;
public class BookingDomainException : Exception
{
    public BookingDomainException()
    {
    }

    public BookingDomainException(string? message) : base(message)
    {
    }

    public BookingDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
