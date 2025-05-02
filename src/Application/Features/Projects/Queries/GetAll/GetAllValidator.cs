using FluentValidation;

namespace Application.Features.Projects.Queries.GetAll;

public class GetAllValidator : AbstractValidator<GetAllQuery>
{
    public GetAllValidator()
    {
        // Şimdilik parametre yok ama yapı korunur.
        // İleride filtreleme, paging, sort eklenirse buraya taşınır.
    }
}