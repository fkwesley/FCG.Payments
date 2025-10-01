using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments();
        Payment GetPaymentById(int id);
        Payment AddPayment(Payment payment);
        Payment UpdatePayment(Payment payment);
        bool DeletePayment(int id);
    }
}
