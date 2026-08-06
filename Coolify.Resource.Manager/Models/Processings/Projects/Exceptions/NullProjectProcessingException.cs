// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Projects.Exceptions
{
    public class NullProjectProcessingException : Xeption
    {
        public NullProjectProcessingException(string message)
            : base(message)
        { }
    }
}
