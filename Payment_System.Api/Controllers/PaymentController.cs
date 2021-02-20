using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payment_System.Domain.Dtos;
using Payment_System.Domain.Interfaces;
using System.Collections.Generic;

namespace Payment_System.Api.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentInterface _paymentRepository;
        public PaymentController(IPaymentInterface paymentInterface)
        {
            _paymentRepository = paymentInterface;
        }

        public IActionResult ProcessPayment(Payment_Dto model, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _paymentRepository.ValidateModel(model);
            if (!validationResult.Item1)
            {
                foreach (KeyValuePair<string, string> error in validationResult.Item2)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            var result = _paymentRepository.ChoosePaymentHandler(model);
            return Ok(result);
        }
    }
}
