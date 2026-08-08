// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class DatabaseValidationException : Xeption
    {
        public DatabaseValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
