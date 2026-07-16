using System.Net;

namespace PersonalFinance.Exception.ExceptionBase;

public class ErrorOnValidationException(List<string> errorsMessages) : ExceptionBase(message: string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors() => errorsMessages;
}