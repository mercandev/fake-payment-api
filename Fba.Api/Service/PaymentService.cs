using System;
using Fba.Api.Const;
using Fba.Api.Domain;
using Fba.Api.Exceptions;
using Fba.Api.Helper;
using Fba.Api.SharedObject;

namespace Fba.Api.Service
{
    public class PaymentService : IPaymentService
    {
        public async Task<ReturnState<object>> StartNone3DsPayment(PaymentViewModel model)
        {
            if (model.Price == default) throw new HbaBusinessException(ErrorConst.PRICEDEFAULT_ERROR);

            Card card = new Card(model.Card.CustomerName, model.Card.CardNumber, model.Card.LastUseMount, model.Card.LastUseYear, model.Card.Cvv);

            card.CheckCardLastUseMount();

            card.CheckCardLastUseYears();

            card.DelegateCardPaymentType();

            card.RandomDelegateCardType();

            var paymentResult = await RestRequestHelper<ReturnState<PaymentResponseViewModel>, PaymentViewModel>.PostService(UrlConst.CREATEPAYMENT_URL , model);

            if (paymentResult == null) throw new HbaBusinessException("Hares Bank connection fail");

            if (!string.IsNullOrWhiteSpace(paymentResult.ErrorMessage)) throw new HbaBusinessException(paymentResult.ErrorMessage);

            return new ReturnState<object>(new PaymentResponseViewModel { PaymentRefNo = paymentResult.Data.PaymentRefNo , PaymentResult = paymentResult.Data.PaymentResult});
        }

        public Task<ReturnState<object>> Start3DsPayment(PaymentViewModel model)
        {
            throw new NotImplementedException();
        }

    }
}

