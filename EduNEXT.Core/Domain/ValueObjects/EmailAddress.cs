using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Errors;
using Primitives;

namespace EduNEXT.Core.Domain.ValueObjects;

public class EmailAddress : ValueObject
{
    private static readonly Regex Regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    
    public string Value { get; private set; }
    
    private EmailAddress(string value)
    {
        Value = value;
    }

    public static Result<EmailAddress, Error> Create(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            return DomainErrors.Student.EmailIsIncorrect;
        }
        
        var normalizedEmail = emailAddress.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(normalizedEmail))
        {
            return DomainErrors.Student.EmailIsIncorrect;
        }
        
        return new EmailAddress(normalizedEmail);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString() => Value;
}