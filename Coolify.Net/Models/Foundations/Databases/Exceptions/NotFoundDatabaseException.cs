// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class NotFoundDatabaseException : Xeption
    {
        public NotFoundDatabaseException(string message)
            : base(message)
        { }
    }
}
