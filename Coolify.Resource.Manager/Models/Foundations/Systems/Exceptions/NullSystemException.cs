// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Systems.Exceptions
{
    public class NullSystemException : Xeption
    {
        public NullSystemException(string message)
            : base(message)
        { }
    }
}
