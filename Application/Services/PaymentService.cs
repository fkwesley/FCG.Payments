using Application.DTO.Payment;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappings;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILoggerService _loggerService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public PaymentService(
                IPaymentRepository paymentRepository, 
                ILoggerService loggerService,
                IHttpContextAccessor httpContext,
                IServiceScopeFactory scopeFactory)
        {
            _paymentRepository = paymentRepository
                ?? throw new ArgumentNullException(nameof(paymentRepository));
            _loggerService = loggerService;
            _httpContext = httpContext;
            _scopeFactory = scopeFactory;
        }

        public async Task<IEnumerable<PaymentResponse>> GetAllPaymentsAsync()
        {
            var payments = _paymentRepository.GetAllPayments();

            using var scope = _scopeFactory.CreateScope();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService>();

            await loggerService.LogTraceAsync(new Trace
            {
                LogId = _httpContext.HttpContext?.Items["RequestId"] as Guid?,
                Timestamp = DateTime.UtcNow,
                Level = LogLevel.Info,
                Message = "Retrieved all payments",
                StackTrace = null
            });

            return payments.Select(payment => payment.ToResponse()).ToList();
        }

        public PaymentResponse GetPaymentById(int id)
        {
            var paymentFound = _paymentRepository.GetPaymentById(id);

            return paymentFound.ToResponse();
        }

        public PaymentResponse AddPayment(AddPaymentRequest payment)
        {
            //verifying if already exists active payment for this order
            var existingPayments = _paymentRepository.GetAllPayments().Where(p => p.OrderId == payment.OrderId && p.Status != PaymentStatus.Completed);
            var paymentEntity = payment.ToEntity();

            var paymentAdded = _paymentRepository.AddPayment(paymentEntity);

            return paymentAdded.ToResponse();
        }

        public PaymentResponse UpdatePayment(UpdatePaymentRequest payment)
        {
            var paymentEntity = payment.ToEntity();
            var paymentUpdated = _paymentRepository.UpdatePayment(paymentEntity);

            return paymentUpdated.ToResponse();
        }

        public bool DeletePayment(int id)
        {
            return _paymentRepository.DeletePayment(id);
        }

    }
}
