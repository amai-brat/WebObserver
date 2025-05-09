using FluentResults;
using FluentValidation;

namespace WebObserver.Main.Application.Helpers;

public static class ValidatorHelper
{
    public static Result ToValidationResult<T>(this IEnumerable<IValidator<T>> validators, T toValidate)
    {
        var vals = validators.ToList();
        if (vals.Count == 0)
        {
            return Result.Ok();
        }
        
        var errorsDictionary = vals
            .Select(x => x.Validate(toValidate))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        return errorsDictionary.Count == 0 
            ? Result.Ok() 
            : Result.Fail(errorsDictionary.Select(kvp => $"{kvp.Key} : ${string.Join("\n", kvp.Value)}").ToList());
    }
}