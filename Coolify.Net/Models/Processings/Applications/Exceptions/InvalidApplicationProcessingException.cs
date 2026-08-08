// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Applications.Exceptions
{
    public class InvalidApplicationProcessingException : Xeption
    {
        public InvalidApplicationProcessingException(string message)
            : base(message)
        { }
    }
}
