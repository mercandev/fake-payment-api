using System;
namespace Fba.Api.Const
{
	public static class ErrorConst
	{
        public const string CUSTOMER_NAME_ERROR = "Customer Name cannot be a null or empty!";
        public const string CARDNUMBER_ERROR = "Card Number cannot be greater than or less than 16";
        public const string CARDCVV_ERROR = "CVV cannot be greater than or less than 3";
        public const string LASTUSEYEAR_ERROR = "LastUseYear only equal this year!";
        public const string LASTUSEMOUNT_ERROR = "LastUseMount must be between 1 and 12";
        public const string CARDPAYMENTTYPE_ERROR = "Unexpected payment method type!";

    }
}

