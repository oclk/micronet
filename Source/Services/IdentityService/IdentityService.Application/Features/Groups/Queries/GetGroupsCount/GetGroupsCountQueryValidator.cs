using FluentValidation;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

/// <summary>
/// Validator class for validating the parameters of the GetGroupsCountQuery.
/// </summary>
public class GetGroupsCountQueryValidator : AbstractValidator<GetGroupsCountQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupsCountQueryValidator"/> class.
    /// </summary>
    public GetGroupsCountQueryValidator()
    {
        // Rule for validating that the Realm property is not empty.
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");

        // You can add more rules here for other properties of GetGroupsCountQuery if needed.
    }
}
