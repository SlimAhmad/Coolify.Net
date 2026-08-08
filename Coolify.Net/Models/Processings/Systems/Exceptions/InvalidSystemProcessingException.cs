// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Systems.Exceptions
{
    public class InvalidSystemProcessingException : Xeption
    {
        public InvalidSystemProcessingException(string message)
            : base(message)
        { }
    }
}
