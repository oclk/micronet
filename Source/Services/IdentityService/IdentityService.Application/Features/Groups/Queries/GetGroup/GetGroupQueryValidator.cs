using FluentValidation;

namespace IdentityService.Application.Features.Groups.Queries.GetGroup;

/// <summary>
/// Validator class for validating the parameters of the GetGroupQuery.
/// </summary>
public class GetGroupQueryValidator : AbstractValidator<GetGroupQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupQueryValidator"/> class.
    /// </summary>
    public GetGroupQueryValidator()
    {
        // Rule for validating that the Realm property is not empty.
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");

        // Rule for validating that the Id property is not empty.
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        // You can add more rules here for other properties of GetGroupQuery if needed.
    }
}
