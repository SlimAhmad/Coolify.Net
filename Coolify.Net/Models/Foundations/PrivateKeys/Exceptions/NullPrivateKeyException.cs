// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.PrivateKeys.Exceptions
{
    public class NullPrivateKeyException : Xeption
    {
        public NullPrivateKeyException(string message)
            : base(message)
        { }
    }
}
