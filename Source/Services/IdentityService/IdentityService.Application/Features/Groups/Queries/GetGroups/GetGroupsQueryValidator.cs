using FluentValidation;

namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

/// <summary>
/// Validator class for validating the parameters of the GetGroupsQuery.
/// </summary>
public class GetGroupsQueryValidator : AbstractValidator<GetGroupsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupsQueryValidator"/> class.
    /// </summary>
    public GetGroupsQueryValidator()
    {
        // Rule for validating that the Realm property is not empty.
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");

        // You can add more rules here for other properties of GetGroupsQuery if needed.
    }
}
