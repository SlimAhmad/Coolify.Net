// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Servers.Exceptions
{
    public class AlreadyExistsServerException : Xeption
    {
        public AlreadyExistsServerException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
