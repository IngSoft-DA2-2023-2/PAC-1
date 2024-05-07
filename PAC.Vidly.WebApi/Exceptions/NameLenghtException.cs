namespace PAC.Vidly.WebApi.Exceptions;

public class NameLenghtException : Exception
{
    public NameLenghtException(string? message) : base(message)
    {
    }
}