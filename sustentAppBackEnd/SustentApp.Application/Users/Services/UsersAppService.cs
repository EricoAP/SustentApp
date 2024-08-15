using Mapster;
using Microsoft.Extensions.Logging;
using SustentApp.Application.Users.Services.Abstractions;
using SustentApp.Application.Utils.Transactions.Abstractions;
using SustentApp.DataTransfer.Users.Requests;
using SustentApp.DataTransfer.Users.Responses;
using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Users.Repositories.Abstractions;
using SustentApp.Domain.Users.Services.Abstractions;
using SustentApp.Domain.Users.Services.Commands;
using SustentApp.Domain.Utils.Queries.Entities;

namespace SustentApp.Application.Users.Services;

public class UsersAppService : IUsersAppService
{
    private readonly IUsersService _usersService;
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<UsersAppService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UsersAppService(IUsersService usersService, IUsersRepository usersRepository, ILogger<UsersAppService> logger, IUnitOfWork unitOfWork)
    {
        _usersService = usersService;
        _usersRepository = usersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResponse> CreateAsync(UserCreateRequest request)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            CreateUserCommand command = request.Adapt<CreateUserCommand>();

            User user = await _usersService.CreateAsync(command);

            await _unitOfWork.CommitAsync();
            return user.Adapt<UserResponse>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "<{Context}> - Error while creating a new user", nameof(UsersAppService));
            throw;
        }
    }

    public PagedList<UserResponse> GetAll(QueryFilter filter)
    {
        IQueryable<User> query = _usersRepository.Query();

        PagedList<User> users = _usersRepository.Paginate(query, filter.Page, filter.PageSize, filter.OrderBy, filter.OrderType);

        return users.Adapt<PagedList<UserResponse>>();
    }

    public UserResponse GetById(string id)
    {
        User user = _usersService.GetById(id);
        return user.Adapt<UserResponse>();
    }

    public AuthenticationResponse SignIn(UserSignInRequest request)
    {
        Authentication authentication = _usersService.SignIn(request.Email, request.Password);
        return authentication.Adapt<AuthenticationResponse>();
    }

    public async Task<UserResponse> UpdateAsync(string id, UserUpdateRequest request)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            UpdateUserCommand command = request.Adapt<UpdateUserCommand>();
            command.Id = id;

            User user = await _usersService.UpdateAsync(command);

            await _unitOfWork.CommitAsync();
            return user.Adapt<UserResponse>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "<{Context}> - Error while updating user", nameof(UsersAppService));
            throw;
        }
    }

    public async Task ConfirmEmailAsync(UserConfirmEmailRequest request)
    {
        try
        {
            _usersService.ConfirmEmail(request.Email, request.Code);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "<{Context}> - Error while confirming user e-mail", nameof(UsersAppService));
            throw;
        }
    }

    public async Task ForgetPasswordAsync(UserForgetPasswordRequest request)
    {
        await _usersService.ForgetPasswordAsync(request.Email);
    }

    public async Task ResetPasswordAsync(UserResetPasswordRequest request)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            _usersService.ResetPassword(request.Email, request.Code, request.Password);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "<{Context}> - Error while reseting user password.", nameof(UsersAppService));
            throw;
        }
    }
}
