// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions
{
    public class FailedPrivateKeyServiceException : Xeption
    {
        public FailedPrivateKeyServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
