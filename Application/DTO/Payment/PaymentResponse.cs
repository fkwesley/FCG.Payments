using Domain.Enums;

namespace Application.DTO.Payment
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public required PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
        public required PaymentMethod PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
