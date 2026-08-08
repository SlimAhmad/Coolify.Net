// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.PrivateKeys.Exceptions
{
    public class PrivateKeyDependencyValidationException : Xeption
    {
        public PrivateKeyDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
