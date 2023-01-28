using System;
namespace Fba.Api.Exceptions
{
    [Serializable]
    public class HbaBusinessException : Exception
	{
        public HbaBusinessException() : base()
        {

        }

        public HbaBusinessException(string message) : base(message)
        {
        }
    }
}

