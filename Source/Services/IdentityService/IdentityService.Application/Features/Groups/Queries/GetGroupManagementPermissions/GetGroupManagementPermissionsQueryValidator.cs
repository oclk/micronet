using FluentValidation;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupManagementPermissions;

/// <summary>
/// Validator class for validating the parameters of the GetGroupManagementPermissionsQuery.
/// </summary>
public class GetGroupManagementPermissionsQueryValidator : AbstractValidator<GetGroupManagementPermissionsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupManagementPermissionsQueryValidator"/> class.
    /// </summary>
    public GetGroupManagementPermissionsQueryValidator()
    {
        // Rule for validating that the Realm property is not empty.
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");

        // Rule for validating that the Id property is not empty.
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        // You can add more rules here for other properties of GetGroupManagementPermissionsQuery if needed.
    }
}
