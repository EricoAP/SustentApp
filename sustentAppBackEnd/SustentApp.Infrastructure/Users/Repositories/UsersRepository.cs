using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Users.Repositories.Abstractions;
using SustentApp.Infrastructure.Contexts;
using SustentApp.Infrastructure.Utils.Repositories;

namespace SustentApp.Infrastructure.Users.Repositories;

public class UsersRepository : EFGenericRepository<User>, IUsersRepository
{
    public UsersRepository(SustentAppContext context) : base(context) { }
}
