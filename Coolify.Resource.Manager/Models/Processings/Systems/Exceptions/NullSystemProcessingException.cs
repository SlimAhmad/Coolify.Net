// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Systems.Exceptions
{
    public class NullSystemProcessingException : Xeption
    {
        public NullSystemProcessingException(string message)
            : base(message)
        { }
    }
}
