#pragma warning disable SA1402 // File may only contain a single type
namespace Ollegorn.LocalExperience.Web.Models;

public record CreateBookingDto(
  DateTime ReservedFor,
  string CustomersName,
  int NumberOfPeople,
  decimal TotalPrice,
  string CustomersNumber,
  string CustomersEmail,
  string Coupon);

public record UpdateBookingDto(
  DateTime ReservedFor,
  string CustomersName,
  int NumberOfPeople,
  decimal TotalPrice,
  string CustomersNumber,
  string CustomersEmail,
  string Coupon);

public record RetrieveBookingDto(
  Guid Id,
  DateTime ReservedFor,
  string CustomersName,
  int NumberOfPeople,
  decimal TotalPrice,
  string CustomersNumber,
  string CustomersEmail,
  string Coupon);
