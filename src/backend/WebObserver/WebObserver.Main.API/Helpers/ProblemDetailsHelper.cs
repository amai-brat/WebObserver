using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WebObserver.Main.API.Helpers;

public static class ProblemDetailsHelper
{
    public static ProblemDetails ToProblemDetails(this List<IError> errors)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = "One or more errors occurred during signup.",
            Extensions =
            {
                ["errors"] = errors.Select(e => new 
                { 
                    e.Message, e.Metadata 
                }).ToList()
            }
        };

        return problemDetails;
    }
}