﻿using FluentValidation;
using Goldlight.Blazor.VirtualServer.Models.RequestResponse;

namespace Goldlight.Blazor.VirtualServer.Pages.RequestResponsePairs.Validator;

public class HeaderValidator : AbstractValidator<HttpHeader>
{
  public HeaderValidator()
  {
    RuleFor(hdr => hdr.Name).NotEmpty();
    RuleFor(hdr => hdr.Value).NotEmpty();
  }
}