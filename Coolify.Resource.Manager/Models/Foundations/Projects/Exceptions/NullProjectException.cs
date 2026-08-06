// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions
{
    public class NullProjectException : Xeption
    {
        public NullProjectException(string message)
            : base(message)
        { }
    }
}
