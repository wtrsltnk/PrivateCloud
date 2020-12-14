using System;
using System.Runtime.Serialization;

namespace PrivateCloud.Practises.Exceptions
{
    [Serializable]
    public sealed class NotFoundException :
        Exception
    {
        public NotFoundException()
        { }

        public NotFoundException(
            string message,
            Exception innerException) :
            base(message, innerException)
        { }

        private NotFoundException(
            SerializationInfo info,
            StreamingContext context) :
            base(info, context)
        { }
    }
}
