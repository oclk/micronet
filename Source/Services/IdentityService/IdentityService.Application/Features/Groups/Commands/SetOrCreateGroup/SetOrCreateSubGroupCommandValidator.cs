using FluentValidation;

namespace IdentityService.Application.Features.Groups.Commands.SetOrCreateGroup;

/// <summary>
/// Validator class for validating the parameters of the SetOrCreateSubGroupCommand.
/// </summary>
public class SetOrCreateSubGroupCommandValidator : AbstractValidator<SetOrCreateSubGroupCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SetOrCreateSubGroupCommandValidator"/> class.
    /// </summary>
    public SetOrCreateSubGroupCommandValidator()
    {
        // Rule for validating that the Realm property is not empty.
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");

        // Rule for validating that the Id property is not empty.
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        // You can add more rules here for other properties of SetOrCreateSubGroupCommand if needed.
    }
}
