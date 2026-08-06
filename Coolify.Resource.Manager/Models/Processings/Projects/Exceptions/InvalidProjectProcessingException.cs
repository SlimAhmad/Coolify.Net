// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Projects.Exceptions
{
    public class InvalidProjectProcessingException : Xeption
    {
        public InvalidProjectProcessingException(string message)
            : base(message)
        { }
    }
}
