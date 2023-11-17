using FluentValidation;

namespace IdentityService.Application.Features.Groups.Commands.CreateGroup;

/// <summary>
/// Validator class for validating the parameters of the CreateGroupCommand.
/// </summary>
public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateGroupCommandValidator"/> class.
    /// </summary>
	public CreateGroupCommandValidator()
	{
        // Rule for validating that the Realm property is not empty.
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");

        // You can add more rules here for other properties of DeleteGroupCommand if needed.
    }
}
