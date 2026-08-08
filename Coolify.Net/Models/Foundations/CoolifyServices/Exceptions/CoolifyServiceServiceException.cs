// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.CoolifyServices.Exceptions
{
    public class CoolifyServiceServiceException : Xeption
    {
        public CoolifyServiceServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
