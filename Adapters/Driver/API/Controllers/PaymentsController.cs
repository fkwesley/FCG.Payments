using API.Models;
using Application.DTO.Payment;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        #region GETS
        /// <summary>
        /// Returns all payments registered.
        /// </summary>
        /// <returns>List of Payments</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        /// <summary>
        /// Returms a payment by id.
        /// </summary>
        /// <returns>Object User</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var payment = _paymentService.GetPaymentById(id);
            return Ok(payment);
        }
        #endregion

        #region POST
        /// <summary>
        /// Add a payment.
        /// </summary>
        /// <returns>Object payment added</returns>
        [HttpPost(Name = "Payment")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] AddPaymentRequest paymentRequest)
        {
            // getting user_email from context (provided by token)
            paymentRequest.Email = HttpContext.User?.FindFirst("user_email")?.Value;

            var createdPayment = _paymentService.AddPayment(paymentRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdPayment.OrderId }, createdPayment);
        }
        #endregion

        #region PUT
        /// <summary>
        /// Update a payment.
        /// </summary>
        /// <returns>Object Payment updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromQuery] PaymentStatus paymentStatus)
        {
            UpdatePaymentRequest paymentRequest = new UpdatePaymentRequest()
            {
                PaymentId = id,
                Status = paymentStatus,
                Email = HttpContext.User?.FindFirst("user_email")?.Value
            };

            var updated = _paymentService.UpdatePayment(paymentRequest);
            return Ok(updated);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete a payment.
        /// </summary>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _paymentService.DeletePayment(id);
            return NoContent();
        }
        #endregion
    }
}
