using System;
using Fba.Api.Helper;

namespace Fba.Api.SharedObject
{
    public class CardViewModel
    {
        [CustomFbaValidation]
        public string CustomerName { get; set; }

        [CustomFbaValidation]
        public string CardNumber { get; set; }

        [CustomFbaValidation]
        public int LastUseMount { get; set; }

        [CustomFbaValidation]
        public int LastUseYear { get; set; }

        [CustomFbaValidation]
        public int Cvv { get; set; }
    }
}

