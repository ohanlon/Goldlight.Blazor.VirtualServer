using FluentValidation;
using FluentValidation.Results;
using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.Pages.Settings.Validator;

public class OrganizationMemberValidator : AbstractValidator<OrganizationMember>
{
  public OrganizationMemberValidator()
  {
    RuleFor(om => om.EmailAddress).NotEmpty().EmailAddress();
  }

  public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
  {
    ValidationResult? result =
      await ValidateAsync(ValidationContext<OrganizationMember>.CreateWithOptions((OrganizationMember)model,
        context => context.IncludeProperties(propertyName)));
    return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
  };
}