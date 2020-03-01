using System;

namespace Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) :
            base(message, innerException) { }

        public NotFoundException(Guid id, Type type) :
            base($"Could not find entity of type {type.Name} with id {id}") { }
    }
}



