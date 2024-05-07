namespace PAC.Vidly.WebApi.Exceptions;

public class DuplicatedException : Exception
{
    public DuplicatedException(string? message) : base(message)
    {
    }
}