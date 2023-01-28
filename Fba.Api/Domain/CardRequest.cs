using System;
using Fba.Api.Helper;

namespace Fba.Api.Domain
{
    public class CardRequest : BaseMartenModel
    {
        public CardRequest()
        {
            Id = Guid.NewGuid();
        }

        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public int LastUseMount { get; set; }
        public int LastUseYear { get; set; }
        public int Cvv { get; set; }
        public CardPaymentType CardPaymentType { get; set; }
        public CardType CardType { get; set; }
    }
}

