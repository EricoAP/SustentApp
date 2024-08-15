namespace SustentApp.Domain.Utils.Exceptions;

public class InvalidAttributeException : DomainException
{
    public InvalidAttributeException() : base("Invalid attribute.") { }
    public InvalidAttributeException(string message) : base(message) { }
}
