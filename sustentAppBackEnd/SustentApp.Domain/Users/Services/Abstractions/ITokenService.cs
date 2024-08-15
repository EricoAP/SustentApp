using SustentApp.Domain.Users.Entities;

namespace SustentApp.Domain.Users.Services.Abstractions;

public interface ITokenService
{
    string GenerateToken(User user);
}
