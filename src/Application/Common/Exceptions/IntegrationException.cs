namespace Delex_POS.Application.Common.Exceptions;

public class IntegrationException : Exception
{
    public IntegrationException(string application)
        : base($"{application} integration issues have occurred.") { }

    public IntegrationException(string application, string message)
        : base($"{application} integration issues have occurred. {message}") { }

    public IntegrationException(string application, Exception inner)
        : base($"{application} integration issues have occurred.", inner) { }
}
