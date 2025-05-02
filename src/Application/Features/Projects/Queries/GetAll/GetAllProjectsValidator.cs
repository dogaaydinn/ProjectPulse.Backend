using FluentValidation;

namespace Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsValidator : AbstractValidator<GetAllProjectsQuery>
{
    public GetAllProjectsValidator()
    {

    }
}