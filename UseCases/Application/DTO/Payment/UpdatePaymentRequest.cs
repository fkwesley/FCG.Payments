using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTO.Payment
{
    public class UpdatePaymentRequest
    {
        [JsonIgnore]
        public int PaymentId { get; set; }
        [JsonIgnore]
        public string? Email { get; set; }
        public required PaymentStatus Status { get; set; }

    }
}
