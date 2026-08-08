// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Applications.Exceptions
{
    public class NotFoundApplicationException : Xeption
    {
        public NotFoundApplicationException(string message)
            : base(message)
        { }
    }
}
