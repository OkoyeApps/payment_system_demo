using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payment_System.Domain.Dtos;
using Payment_System.Domain.Interfaces;
using Payment_System.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payment_System.Api.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentInterface _paymentRepository;
        private readonly IMapper _mapper;
        private readonly PaymentModelValidators validators = new PaymentModelValidators();
        public PaymentController(IPaymentInterface paymentInterface, IMapper mapper)
        {
            _paymentRepository = paymentInterface;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name ="get_payment_status")]
        public async Task<IActionResult> Get (Guid id)
        {
            var result = await _paymentRepository.GetPaymentByIdAsync(id);

            return Ok(_mapper.Map<PaymentDetailResponse>(result));
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(Payment_Dto model, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = validators.ValidateModel(model);
            if (!validationResult.Item1)
            {
                foreach (KeyValuePair<string, string> error in validationResult.Item2)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            var result = await _paymentRepository.ChoosePaymentHandler(model);
            Response.Headers.Add("X-Resource", Url.Action("Get", new { id = result.Id}));
            return Created("get_payment_status", result);
        }
    }
}
