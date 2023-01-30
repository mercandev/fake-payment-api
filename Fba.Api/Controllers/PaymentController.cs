using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Fba.Api.Service;
using Fba.Api.SharedObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Fba.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ReturnState<object>> PostCreateNoneSecurePayment(PaymentViewModel model)
            => await _paymentService.StartNone3DsPayment(model);

        [HttpPost]
        public async Task<ReturnState<object>> PostCreate3dSecurePayment(PaymentViewModel model)
            => await _paymentService.Start3DsPayment(model);

        [HttpGet]
        public async Task<ReturnState<object>> GetComplate3dSecurePayment(string code , string paymentUniqueId)
            => await _paymentService.Complate3DsPayment(code, paymentUniqueId);
    }
}

