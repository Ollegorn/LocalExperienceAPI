namespace Ollegorn.LocalExperience.API.Endpoints;

using System.ComponentModel.DataAnnotations;

using Asp.Versioning;

using FluentValidation;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using Ollegorn.LocalExperience.API.Extensions;
using Ollegorn.LocalExperience.API.Extentions;
using Ollegorn.LocalExperience.Persistence;
using Ollegorn.LocalExperience.Persistence.Models;
using Ollegorn.LocalExperience.Web.Models;

public class BookingEndpoints(ILogger<BookingEndpoints> logger) : IEndpoint
{
  private readonly ILogger logger = logger;

  public void MapEndpoint(IEndpointRouteBuilder app)
  {
    ArgumentNullException.ThrowIfNull(app);

    var api = app.NewApiVersionSet()
      .HasApiVersion(new ApiVersion(1))
      .Build();

    var group = app
      .MapGroup("api/v1/booking") // {apiVersion}
      .WithApiVersionSet(api)
      .HasApiVersion(1)
      .WithTags("Booking")
      .WithOpenApi();

    group.MapGet("{id:Guid}", GetBooking).WithName(nameof(GetBooking));
    group.MapGet(string.Empty, GetAllBookings);
    group.MapPost(string.Empty, CreateBooking);
    group.MapDelete("{id:Guid}", DeleteBooking);
    group.MapPut("{id:Guid}", UpdateBooking);
  }

  public async Task<Results<Ok<RetrieveBookingDto>, NotFound>> GetBooking(Guid id, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var booking = await dbContext.Bookings.Include(b => b.Activity).FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    if (booking == null)
    {
      return TypedResults.NotFound();
    }

    var bookingDto = booking.ToRetrieveBookingDto();
    return TypedResults.Ok(bookingDto);
  }

  public async Task<Ok<ICollection<RetrieveBookingDto>>> GetAllBookings(LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var bookings = await dbContext.Bookings.Include(b => b.Activity).ToListAsync(cancellationToken);
    var dtos = bookings.ToRetrieveBookingDtoList();

    return TypedResults.Ok(dtos);
  }

  public async Task<Results<CreatedAtRoute<RetrieveBookingDto>, BadRequest, ValidationProblem>> CreateBooking(CreateBookingDto createBookingDto, LocalExperienceDbContext dbContext, IValidator<CreateBookingDto> validator, CancellationToken cancellationToken)
  {
    var validationResult = await validator.ValidateAsync(createBookingDto, cancellationToken);
    var activity = await dbContext.Activities.FindAsync(createBookingDto.ActivityId);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var booking = new Booking
    {
      ReservedFor = createBookingDto.ReservedFor,
      CustomersName = createBookingDto.CustomersName,
      NumberOfPeople = createBookingDto.NumberOfPeople,
      TotalPrice = createBookingDto.TotalPrice,
      CustomersNumber = createBookingDto.CustomersNumber,
      CustomersEmail = createBookingDto.CustomersEmail,
      Coupon = createBookingDto.Coupon,
      Activity = activity
    };

    dbContext.Bookings.Add(booking);
    await dbContext.SaveChangesAsync(cancellationToken);

    var bookingDto = booking.ToRetrieveBookingDto();

    return TypedResults.CreatedAtRoute(bookingDto, nameof(GetBooking), new { id = booking.Id });
  }

  public async Task<Results<NoContent, NotFound>> DeleteBooking(Guid id, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var booking = await dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    if (booking is null)
    {
      return TypedResults.NotFound();
    }

    dbContext.Bookings.Remove(booking);
    await dbContext.SaveChangesAsync(cancellationToken);

    return TypedResults.NoContent();
  }

  public async Task<Results<Ok<RetrieveBookingDto>, NotFound>> UpdateBooking(Guid id, UpdateBookingDto updateBookingDto, LocalExperienceDbContext dbContext, CancellationToken cancellationToken)
  {
    var booking = await dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    var activity = await dbContext.Activities.FindAsync(updateBookingDto.ActivityId);
    if (booking is null)
    {
      return TypedResults.NotFound();
    }

    booking.ReservedFor = updateBookingDto.ReservedFor;
    booking.CustomersName = updateBookingDto.CustomersName;
    booking.NumberOfPeople = updateBookingDto.NumberOfPeople;
    booking.TotalPrice = updateBookingDto.TotalPrice;
    booking.CustomersNumber = updateBookingDto.CustomersNumber;
    booking.CustomersEmail = updateBookingDto.CustomersEmail;
    booking.Coupon = updateBookingDto.Coupon;
    booking.Activity = activity;

    await dbContext.SaveChangesAsync(cancellationToken);

    var bookingDto = booking.ToRetrieveBookingDto();
    return TypedResults.Ok(bookingDto);
  }
}
