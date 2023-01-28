using System;
namespace Fba.Api.SharedObject
{
    public class PaymentViewModel
    {
        public decimal Price { get; set; }
        public CardViewModel Card { get; set; }
    }
}

