using System;
using Fba.Api.SharedObject;

namespace Fba.Api.Service
{
    public interface IPaymentService
    {
        Task<ReturnState<object>> StartNone3DsPayment(PaymentViewModel model);
        Task<ReturnState<object>> Start3DsPayment(PaymentViewModel model);
    }
}

