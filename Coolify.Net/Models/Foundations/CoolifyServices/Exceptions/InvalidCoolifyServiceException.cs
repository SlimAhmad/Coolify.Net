// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.CoolifyServices.Exceptions
{
    public class InvalidCoolifyServiceException : Xeption
    {
        public InvalidCoolifyServiceException(string message)
            : base(message)
        { }

        public InvalidCoolifyServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
