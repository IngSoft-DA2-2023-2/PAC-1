namespace PAC.Vidly.WebApi.Exceptions;

public class ArgsNullException : Exception
{
    public ArgsNullException(string? message) : base(message)
    {
    }
}