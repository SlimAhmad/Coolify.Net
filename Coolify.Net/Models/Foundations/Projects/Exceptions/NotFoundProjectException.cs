// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Projects.Exceptions
{
    public class NotFoundProjectException : Xeption
    {
        public NotFoundProjectException(string message)
            : base(message)
        { }
    }
}
