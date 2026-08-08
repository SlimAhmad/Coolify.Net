// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Systems.Exceptions
{
    public class AlreadyExistsSystemException : Xeption
    {
        public AlreadyExistsSystemException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
