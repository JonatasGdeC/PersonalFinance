using System.Net;

namespace PersonalFinance.Exception.ExceptionBase;

public class InvalidLoginException() : ExceptionBase(message: ResourceErrorMessages.INVALID_LOGIN)
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    
    public override List<string> GetErrors()
    {
        return [Message];
    }
}