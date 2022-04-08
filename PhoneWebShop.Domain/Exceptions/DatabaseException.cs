using System;

namespace PhoneWebShop.Domain.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
            
        }
    }
}
