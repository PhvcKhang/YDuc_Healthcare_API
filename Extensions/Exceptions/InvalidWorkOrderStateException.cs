namespace HealthCareApplication.Extensions.Exceptions;
using System.Runtime.Serialization;

[Serializable]
public class InvalidWorkOrderStateException: Exception
{
    public InvalidWorkOrderStateException()
    {
    }

    public InvalidWorkOrderStateException(string? message) : base(message)
    {
    }

    public InvalidWorkOrderStateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidWorkOrderStateException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
    {
    }
}
