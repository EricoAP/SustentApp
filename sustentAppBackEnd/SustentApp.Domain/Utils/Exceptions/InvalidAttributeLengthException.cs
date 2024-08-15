namespace SustentApp.Domain.Utils.Exceptions;

public class InvalidAttributeLengthException : DomainException
{
    public InvalidAttributeLengthException() : base("Invalid attribute length.") { }

    public InvalidAttributeLengthException(string attributeName, int maxLength) : base($"Invalid length for {attributeName}. Max. Length: {maxLength}") { }

    public InvalidAttributeLengthException(string attributeName, int? minLength, int maxLength) : base($"Invalid length for {attributeName}. Min. Length: {minLength ?? 0}. Max. Length: {maxLength}") { }
}
