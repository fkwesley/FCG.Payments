using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTO.Payment
{
    public class AddPaymentRequest
    {
        [JsonIgnore]
        public int? PaymentId { get; set; }
        public int OrderId { get; internal set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }

        [RegularExpression(@"^\d{4}-\d{4}-\d{4}-\d{4}$", ErrorMessage = "CardNumber must be in the format 0123-4567-8901-2345")]
        [DisplayFormat(DataFormatString = "0123-4567-8901-2345")]
        public string? CardNumber { get; init; }
        public string? CardHolder { get; init; }

        [RegularExpression(@"^\d{4}-\d{2}$", ErrorMessage = "ExpiryDate must be in the format yyyy-MM")]
        [DisplayFormat(DataFormatString = "yyyy-MM")]
        public string? ExpiryDate { get; set; }

        [RegularExpression(@"^\d{3}$", ErrorMessage = "Cvv must be 3 digits")]
        [DisplayFormat(DataFormatString = "123")]
        public string? Cvv { get; init; }
    }
}
