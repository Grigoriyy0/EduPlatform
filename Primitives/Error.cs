using CSharpFunctionalExtensions;

namespace Primitives;

public class Error : ValueObject
{
    public Error(byte serviceCode, string code, string message)
    {
        ServiceCode = serviceCode;
        Code = code;
        Message = message;
    }
    
    public byte ServiceCode { get; set; }
    
    public string Code { get; set; }
    
    public string Message { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Message;
    }
}