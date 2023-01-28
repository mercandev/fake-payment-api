using System;
using Fba.Api.Exceptions;

namespace Fba.Api.Helper
{
	public static class Contract
	{
        public static void IsRequired(bool isSuccess, string message)
        {
            if (!isSuccess)
            {
                return;
            }

            throw new HbaBusinessException(message);
        }
    }
}

