// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Applications.Exceptions
{
    public class NullApplicationProcessingException : Xeption
    {
        public NullApplicationProcessingException(string message)
            : base(message)
        { }
    }
}
