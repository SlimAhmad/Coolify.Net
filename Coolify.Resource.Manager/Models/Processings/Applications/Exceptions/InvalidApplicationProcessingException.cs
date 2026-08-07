// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Applications.Exceptions
{
    public class InvalidApplicationProcessingException : Xeption
    {
        public InvalidApplicationProcessingException(string message)
            : base(message)
        { }
    }
}
