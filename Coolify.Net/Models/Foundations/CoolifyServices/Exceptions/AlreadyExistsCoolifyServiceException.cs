// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.CoolifyServices.Exceptions
{
    public class AlreadyExistsCoolifyServiceException : Xeption
    {
        public AlreadyExistsCoolifyServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
