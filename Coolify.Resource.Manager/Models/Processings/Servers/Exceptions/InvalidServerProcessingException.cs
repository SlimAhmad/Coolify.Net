// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Servers.Exceptions
{
    public class InvalidServerProcessingException : Xeption
    {
        public InvalidServerProcessingException(string message)
            : base(message)
        { }
    }
}
