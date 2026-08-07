// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions
{
    public class FailedPrivateKeyDependencyException : Xeption
    {
        public FailedPrivateKeyDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
