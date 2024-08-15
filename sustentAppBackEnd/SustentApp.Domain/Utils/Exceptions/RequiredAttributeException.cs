namespace SustentApp.Domain.Utils.Exceptions;

public class RequiredAttributeException : DomainException
{
    public RequiredAttributeException() : base("Required attribute.") { }
    public RequiredAttributeException(string attributeName) : base($"{attributeName} is required.") { }
}
