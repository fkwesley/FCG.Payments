using Application.DTO.Payment;
using Domain.Entities;
using Application.Helpers;
using Domain.Enums;

namespace Application.Mappings
{
    public static class OrderMappingExtensions
    {
        /// <summary>
        /// Maps a AddPaymentRequest to a Order entity.
        public static Payment ToEntity(this AddPaymentRequest request)
        {
            return new Payment
            {
                OrderId = request.OrderId,
                PaymentMethod = request.PaymentMethod,
                Amount = request.Amount,
                Status = PaymentStatus.Received,
                CardHolder = request.CardHolder ?? string.Empty,
                CardNumber = request.CardNumber ?? string.Empty,
                Cvv = request.Cvv ?? string.Empty,
                ExpiryDate = request.ExpiryDate ?? string.Empty
            };
        }

        /// <summary>
        /// Maps a UpdatePaymentRequest to a Order entity.
        public static Payment ToEntity(this UpdatePaymentRequest request)
        {
            return new Payment
            {
                PaymentId = request.PaymentId,
                Status = request.Status,
                PaymentMethod = PaymentMethod.Pix //Default value to avoid error, will not be used
            };
        }

        /// <summary>
        /// Maps a Order entity to a PaymentResponse.
        public static PaymentResponse ToResponse(this Payment entity)
        {
            return new PaymentResponse
            {
                PaymentId = entity.PaymentId,
                OrderId = entity.OrderId,
                Amount = entity.Amount,
                PaymentMethod = entity.PaymentMethod,
                Status = entity.Status,
                CreatedAt = DateTimeHelper.ConvertUtcToTimeZone(entity.CreatedAt, "E. South America Standard Time"),
                UpdatedAt = entity.UpdatedAt.HasValue ?
                                DateTimeHelper.ConvertUtcToTimeZone(entity.UpdatedAt.Value, "E. South America Standard Time") : (DateTime?)null,
            };
        }
    }
}
