using SustentApp.DataTransfer.Users.Requests;
using SustentApp.DataTransfer.Users.Responses;
using SustentApp.Domain.Utils.Queries.Entities;

namespace SustentApp.Application.Users.Services.Abstractions;

public interface IUsersAppService
{
    Task<UserResponse> CreateAsync(UserCreateRequest request);
    PagedList<UserResponse> GetAll(QueryFilter filter);
    UserResponse GetById(string id);
    Task<UserResponse> UpdateAsync(string id, UserUpdateRequest request);
    AuthenticationResponse SignIn(UserSignInRequest request);
    Task ConfirmEmailAsync(UserConfirmEmailRequest request);
    Task ForgetPasswordAsync(UserForgetPasswordRequest request);
    Task ResetPasswordAsync(UserResetPasswordRequest request);
}
