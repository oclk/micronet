using FluentValidation;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

public class GetGroupsCountQueryValidator : AbstractValidator<GetGroupsCountQuery>
{
    public GetGroupsCountQueryValidator()
    {
        RuleFor(x => x.Realm)
            .NotEmpty().WithMessage("Realm is required.");
    }
}
