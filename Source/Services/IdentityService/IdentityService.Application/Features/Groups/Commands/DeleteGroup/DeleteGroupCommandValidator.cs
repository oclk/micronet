using FluentValidation;

namespace IdentityService.Application.Features.Groups.Commands.DeleteGroup;

/// <summary>
/// Validator class for validating the parameters of the DeleteGroupCommand.
/// </summary>
public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteGroupCommandValidator"/> class.
    /// </summary>
    public DeleteGroupCommandValidator()
    {
        // Rule for validating that the Realm property is not empty.
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");

        // Rule for validating that the Id property is not empty.
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        // You can add more rules here for other properties of DeleteGroupCommand if needed.
    }
}
