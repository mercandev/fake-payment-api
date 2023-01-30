using System;
using System.Transactions;
using Fba.Api.Const;
using Fba.Api.Domain;
using Fba.Api.Exceptions;
using Fba.Api.Helper;
using Fba.Api.SharedObject;
using Marten;

namespace Fba.Api.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IDocumentSession _documentSession;
        private readonly IQuerySession _querySession;

        public PaymentService(IDocumentSession documentSession, IQuerySession querySession)
        {
            this._documentSession = documentSession;
            this._querySession = querySession;
        }

        public async Task<ReturnState<object>> StartNone3DsPayment(PaymentViewModel model)
        {
            CheckCardInformation(model);

            var paymentResult = await RestRequestHelper<ReturnState<PaymentResponseViewModel>, PaymentViewModel>.PostService(UrlConst.CREATEPAYMENT_URL, model);

            if (paymentResult == null) throw new HbaBusinessException("Hares Bank connection fail"); //real life, this service goes to real payment service :) 

            if (!string.IsNullOrWhiteSpace(paymentResult.ErrorMessage)) throw new HbaBusinessException(paymentResult.ErrorMessage);

            return new ReturnState<object>(new PaymentResponseViewModel { PaymentRefNo = paymentResult.Data.PaymentRefNo, PaymentResult = paymentResult.Data.PaymentResult });
        }

        public async Task<ReturnState<object>> Start3DsPayment(PaymentViewModel model)
        {
            CheckCardInformation(model);

            //Hares Bank request. Card validation!

            SecurePaymentModel paymentModel = new()
            {
                Unique3dsPaymentId = Guid.NewGuid().ToString(),
                PaymentInformations = model,
                SecureCode = GeneratorHelper.DigitGenerator(5)
            };

            _documentSession.Insert(paymentModel);
            await _documentSession.SaveChangesAsync();


            var htmlContent = System.IO.File.ReadAllText(@"./Content/SecurePage.html")
                .Replace("{{0}}", paymentModel.Unique3dsPaymentId.ToString())
                .Replace("\n", @"")
                .Replace("\"", @"'");
                
            SecurePaymentVerificationViewModel securePayment = new()
            {
                IsStatus = true,
                HtmlContent = htmlContent.ToString()
            };

            return new ReturnState<object>(securePayment);
        }

        private void CheckCardInformation(PaymentViewModel model)
        {
            if (model.Price == default) throw new HbaBusinessException(ErrorConst.PRICEDEFAULT_ERROR);

            Card card = new Card(model.Card.CustomerName, model.Card.CardNumber, model.Card.LastUseMount, model.Card.LastUseYear, model.Card.Cvv);

            card.CheckCardLastUseMount();

            card.CheckCardLastUseYears();

            card.DelegateCardPaymentType();

            card.RandomDelegateCardType();
        }

        public async Task<ReturnState<object>> Complate3DsPayment(string code, string paymentUniqueId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(paymentUniqueId)) throw new HbaBusinessException("code or paymentUniqueId is not null!");

            var result = _querySession.Query<SecurePaymentModel>().Where(x => x.Unique3dsPaymentId.ToString() == paymentUniqueId && x.IsActive == true).FirstOrDefault();

            if (result == null) throw new HbaBusinessException("paymentId not valid!");

            if (result.SecureCode != code) throw new HbaBusinessException("code fail!");

            //hares bank connection and payment complate!

            //update complate payment status!

            result.IsActive = false;
            _documentSession.Update(result);
            await _documentSession.SaveChangesAsync();

            return new ReturnState<object>(true);

        }
    }
}

