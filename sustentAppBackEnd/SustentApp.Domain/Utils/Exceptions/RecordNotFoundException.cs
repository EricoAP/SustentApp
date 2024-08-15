namespace SustentApp.Domain.Utils.Exceptions;

public class RecordNotFoundException : DomainException
{
    public RecordNotFoundException() : base("Record not found.") { }

    public RecordNotFoundException(string recordName) : base($"{recordName} not found.") { }
}
