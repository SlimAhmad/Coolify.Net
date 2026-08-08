// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.PrivateKeys.Exceptions
{
    public class FailedPrivateKeyServiceException : Xeption
    {
        public FailedPrivateKeyServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
