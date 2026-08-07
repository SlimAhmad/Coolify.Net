// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Systems.Exceptions
{
    public class InvalidSystemProcessingException : Xeption
    {
        public InvalidSystemProcessingException(string message)
            : base(message)
        { }
    }
}
