using System;
using System.Runtime.Serialization;

namespace Kad.Arbitr.ru
{
    [Serializable]
    internal class NoGoodProxyException : Exception
    {
        public NoGoodProxyException()
        {
        }

        public NoGoodProxyException(string message) : base(message)
        {
        }

        public NoGoodProxyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoGoodProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}