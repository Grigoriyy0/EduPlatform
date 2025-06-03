namespace Primitives;

public class GeneralErrors
{
    public static Error ValueIsIncorrect(byte serviceCode, string valueName)
    {
        ArgumentException.ThrowIfNullOrEmpty(valueName);
        
        return new Error(serviceCode, "value.is.incorrect", $"Value '{valueName}' is incorrect");
    }
}