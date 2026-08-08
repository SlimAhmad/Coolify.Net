// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Servers.Exceptions
{
    public class NullServerException : Xeption
    {
        public NullServerException(string message)
            : base(message)
        { }
    }
}
