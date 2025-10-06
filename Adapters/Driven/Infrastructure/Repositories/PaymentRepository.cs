using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentsDbContext _context;

        public PaymentRepository(PaymentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Payment> GetAllPayments()
        {
           return _context.Payments.ToList();
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments.FirstOrDefault(o => o.PaymentId == id)
                ?? throw new KeyNotFoundException($"Payment with ID {id} not found.");
        }

        public Payment AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public Payment UpdatePayment(Payment payment)
        {
            var existingPayment = GetPaymentById(payment.PaymentId);

            if (existingPayment != null) {
                existingPayment.Status = payment.Status;
                existingPayment.UpdatedAt = DateTime.UtcNow;

                _context.Payments.Update(existingPayment);
                _context.SaveChanges();
            }
            else
                throw new KeyNotFoundException($"Payment with ID {payment.OrderId} not found.");

            return existingPayment;
        }

        public bool DeletePayment(int id)
        {
            var game = GetPaymentById(id);

            if (game != null)
            {
                _context.Payments.Remove(game);
                _context.SaveChanges();
                return true;
            }
            else
                throw new KeyNotFoundException($"Payment with ID {id} not found.");
        }

    }
}
