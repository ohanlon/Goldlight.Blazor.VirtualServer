using FluentValidation;
using FluentValidation.Results;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Components.RequestResponse.Validator;

public class RequestResponsePairValidator : AbstractValidator<RequestResponsePair>
{
  public RequestResponsePairValidator()
  {
    RuleFor(rr => rr.Name).NotEmpty().WithMessage("The name needs a minimum of 10 characters.").MinimumLength(10)
      .WithMessage("The name needs a minimum of 10 characters.");
    RuleFor(rr => rr.Description).NotEmpty().WithMessage("The description needs a minimum of 10 characters.")
      .MinimumLength(10).WithMessage("The description needs a minimum of 10 characters.");
    RuleFor(rr => rr.Response.Summary.Status).NotEmpty();
    RuleFor(rr => rr.Response.Summary.Protocol).NotEmpty();
    RuleFor(rr => rr.Request.Summary.Method).NotEmpty();
    RuleFor(rr => rr.Request.Summary.Path).NotEmpty();
    RuleForEach(rr => rr.Response.Headers).SetValidator(new HeaderValidator());
    RuleForEach(rr => rr.Request.Headers).SetValidator(new HeaderValidator());
  }

  public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
  {
    ValidationResult? result =
      await ValidateAsync(ValidationContext<RequestResponsePair>.CreateWithOptions((RequestResponsePair)model,
        context => context.IncludeProperties(propertyName)));
    return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
  };
}