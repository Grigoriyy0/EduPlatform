using Primitives;

namespace EduNEXT.Core.Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static Error EmailIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.email");
    }
}