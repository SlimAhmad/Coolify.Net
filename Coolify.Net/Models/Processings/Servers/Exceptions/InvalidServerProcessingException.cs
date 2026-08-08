// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Servers.Exceptions
{
    public class InvalidServerProcessingException : Xeption
    {
        public InvalidServerProcessingException(string message)
            : base(message)
        { }
    }
}
