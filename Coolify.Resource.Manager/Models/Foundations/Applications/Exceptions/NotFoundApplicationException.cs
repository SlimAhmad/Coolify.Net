// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions
{
    public class NotFoundApplicationException : Xeption
    {
        public NotFoundApplicationException(string message)
            : base(message)
        { }
    }
}
