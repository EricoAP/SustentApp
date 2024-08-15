using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Utils.Repositories.Abstractions;

namespace SustentApp.Domain.Users.Repositories.Abstractions;

public interface IUsersRepository : IGenericRepository<User>
{
}
