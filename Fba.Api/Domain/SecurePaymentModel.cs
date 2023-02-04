using System;
using Fba.Api.SharedObject;

namespace Fba.Api.Domain
{
	public class SecurePaymentModel : BaseMartenModel
	{
		public string Unique3dsPaymentId { get; set; }
		public string SecureCode { get; set; }
		public PaymentViewModel PaymentInformations { get; set; }
	}
}

