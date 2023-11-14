using FluentValidation;

namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

public class GetGroupsQueryValidator : AbstractValidator<GetGroupsQuery>
{
    public GetGroupsQueryValidator()
    {
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");
    }
}
