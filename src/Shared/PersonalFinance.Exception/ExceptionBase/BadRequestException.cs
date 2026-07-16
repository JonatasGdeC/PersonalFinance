using System.Net;

namespace PersonalFinance.Exception.ExceptionBase;

public class BadRequestException(string message) : ExceptionBase(message: message)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors() => [Message];
}