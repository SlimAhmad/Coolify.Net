// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.PrivateKeys.Exceptions
{
    public class NotFoundPrivateKeyException : Xeption
    {
        public NotFoundPrivateKeyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
