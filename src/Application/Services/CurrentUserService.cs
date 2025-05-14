using Application.Interfaces;

namespace Application.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId { get; }
}