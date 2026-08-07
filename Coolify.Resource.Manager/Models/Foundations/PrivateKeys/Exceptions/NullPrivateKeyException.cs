// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions
{
    public class NullPrivateKeyException : Xeption
    {
        public NullPrivateKeyException(string message)
            : base(message)
        { }
    }
}
