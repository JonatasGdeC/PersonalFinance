using System.Net;

namespace PersonalFinance.Exception.ExceptionBase;

public class NotFoundException(string message) : ExceptionBase(message: message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors() => [Message];
}