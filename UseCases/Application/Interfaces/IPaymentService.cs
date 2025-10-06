using Application.DTO.Payment;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentResponse>> GetAllPaymentsAsync();
        PaymentResponse GetPaymentById(int id);
        PaymentResponse AddPayment(AddPaymentRequest payment);
        PaymentResponse UpdatePayment(UpdatePaymentRequest payment);
        bool DeletePayment(int id);
    }
}
