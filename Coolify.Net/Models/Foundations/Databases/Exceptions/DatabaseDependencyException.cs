// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class DatabaseDependencyException : Xeption
    {
        public DatabaseDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
