using Application.DTO.Payment;
using Application.Helpers;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Mappings
{
    public static class PaymentMappingExtensions
    {
        /// <summary>
        /// Maps a AddPaymentRequest to a Payment entity.
        public static Payment ToEntity(this AddPaymentRequest request)
        {
            var payment = new Payment
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


            if (payment.PaymentMethod != PaymentMethod.Pix)
            {
                if (!payment.IsValidCardNumber())
                    throw new BusinessException("Invalid card number.");

                if (!payment.IsValidExpiryDate())
                    throw new BusinessException("The card has already expired or is invalid. Provide a new card");
            }

            return payment;
        }

        /// <summary>
        /// Maps a UpdatePaymentRequest to a Payment entity.
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
        /// Maps a Payment entity to a PaymentResponse.
        public static PaymentResponse ToResponse(this Payment entity)
        {
            return new PaymentResponse
            {
                PaymentId = entity.PaymentId,
                OrderId = entity.OrderId,
                Amount = entity.Amount,
                PaymentMethod = entity.PaymentMethod,
                Status = entity.Status,
                CreatedAt = DateTimeHelper.ConvertUtcToTimeZone(entity.CreatedAt, "America/Sao_Paulo"),
                UpdatedAt = entity.UpdatedAt.HasValue ?
                                DateTimeHelper.ConvertUtcToTimeZone(entity.UpdatedAt.Value, "America/Sao_Paulo") : (DateTime?)null,
            };
        }
    }
}
