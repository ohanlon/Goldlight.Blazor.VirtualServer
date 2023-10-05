using FluentValidation;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Components.RequestResponse.Validator;

public class HeaderValidator : AbstractValidator<HttpHeader>
{
  public HeaderValidator()
  {
    RuleFor(hdr => hdr.Name).NotEmpty();
    RuleFor(hdr => hdr.Value).NotEmpty();
  }
}