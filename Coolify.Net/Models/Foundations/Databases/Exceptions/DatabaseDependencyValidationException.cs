// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class DatabaseDependencyValidationException : Xeption
    {
        public DatabaseDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
