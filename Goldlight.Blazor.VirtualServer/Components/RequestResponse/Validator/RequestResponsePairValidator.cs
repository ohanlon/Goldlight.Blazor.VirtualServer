using FluentValidation;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Components.RequestResponse.Validator;

public class RequestResponsePairValidator : AbstractValidator<RequestResponsePair>
{
    public RequestResponsePairValidator()
    {
        RuleFor(rr => rr.Name).NotEmpty().MinimumLength(10);
        RuleFor(rr => rr.Description).NotEmpty().MinimumLength(20);
        RuleFor(rr => rr.Response.Summary.Status).NotEmpty();
        RuleFor(rr => rr.Response.Summary.Version).NotEmpty();
        RuleFor(rr => rr.Request.Summary.Method).NotEmpty();
        RuleFor(rr => rr.Request.Summary.Path).NotEmpty();
        RuleForEach(rr => rr.Response.Headers).SetValidator(new HeaderValidator());
        RuleForEach(rr => rr.Request.Headers).SetValidator(new HeaderValidator());
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<RequestResponsePair>.CreateWithOptions((RequestResponsePair)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}