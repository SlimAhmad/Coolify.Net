// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions
{
    public class InvalidPrivateKeyException : Xeption
    {
        public InvalidPrivateKeyException(string message)
            : base(message)
        { }

        public InvalidPrivateKeyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
