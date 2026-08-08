// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Projects.Exceptions
{
    public class InvalidProjectProcessingException : Xeption
    {
        public InvalidProjectProcessingException(string message)
            : base(message)
        { }
    }
}
