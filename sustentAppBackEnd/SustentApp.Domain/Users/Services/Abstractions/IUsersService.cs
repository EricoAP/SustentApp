using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Users.Services.Commands;

namespace SustentApp.Domain.Users.Services.Abstractions;

public interface IUsersService
{
    Task<User> CreateAsync(CreateUserCommand command);
    User GetById(string id);
    Task<User> UpdateAsync(UpdateUserCommand command);
    Authentication SignIn(string email, string password);
    void ConfirmEmail(string email, string code);
    Task ForgetPasswordAsync(string email);
    void ResetPassword(string email, string code, string password);
}
