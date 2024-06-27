namespace Ollegorn.LocalExperience.API.Extentions;

using Ollegorn.LocalExperience.API.Extensions;
using Ollegorn.LocalExperience.Persistence.Models;
using Ollegorn.LocalExperience.Web.Models;

public static class BookingMappingExtentions
{
  public static RetrieveBookingDto ToRetrieveBookingDto(this Booking booking)
  {
    ArgumentNullException.ThrowIfNull(booking);

    var dto = new RetrieveBookingDto(
      booking.Id,
      booking.ReservedFor,
      booking.CustomersName,
      booking.NumberOfPeople,
      booking.TotalPrice,
      booking.CustomersNumber,
      booking.CustomersEmail,
      booking.Coupon,
      booking.Activity.Id);

    return dto;
  }

  public static ICollection<RetrieveBookingDto> ToRetrieveBookingDtoList(this ICollection<Booking> booking)
  {
    ArgumentNullException.ThrowIfNull(booking);

    var dtos = booking.Select(a => a.ToRetrieveBookingDto()).ToList();

    return dtos;
  }
}
