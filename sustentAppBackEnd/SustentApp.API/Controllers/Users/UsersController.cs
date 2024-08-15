using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SustentApp.Application.Users.Services.Abstractions;
using SustentApp.DataTransfer.Users.Requests;
using SustentApp.DataTransfer.Users.Responses;
using SustentApp.Domain.Utils.Queries.Entities;

namespace SustentApp.API.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController(IUsersAppService usersAppService) : ControllerBase
{
    private readonly IUsersAppService _usersAppService = usersAppService;

    /// <summary>
    ///     Get all users by filter.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public ActionResult<PagedList<UserResponse>> GetAll([FromQuery] QueryFilter filter)
    {
        var users = _usersAppService.GetAll(filter);
        return Ok(users);
    }

    /// <summary>
    ///     Get user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<UserResponse> GetById(string id)
    {
        var user = _usersAppService.GetById(id);
        return Ok(user);
    }

    /// <summary>
    ///     Create a new user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserResponse>> CreateAsync([FromBody] UserCreateRequest request)
    {
        var user = await _usersAppService.CreateAsync(request);
        return Ok(user);
    }

    /// <summary>
    ///     Update user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<UserResponse>> UpdateAsync(string id, [FromBody] UserUpdateRequest request)
    {
        var user = await _usersAppService.UpdateAsync(id, request);
        return Ok(user);
    }

    /// <summary>
    ///     Authenticates the user based on their credentials.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult<AuthenticationResponse> SignIn([FromBody] UserSignInRequest request)
    {
        var response = _usersAppService.SignIn(request);
        return Ok(response);
    }

    /// <summary>
    ///     Confirms the user's e-mail address based on the code sent by e-mail
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("confirm-email")]
    [AllowAnonymous]
    public async Task<ActionResult> ConfirmEmailAsync([FromBody] UserConfirmEmailRequest request)
    {
        await _usersAppService.ConfirmEmailAsync(request);
        return Ok();
    }

    /// <summary>
    ///     Sends a password reset e-mail to the user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("forget-password")]
    [AllowAnonymous]
    public async Task<ActionResult> ForgetPasswordAsync([FromBody] UserForgetPasswordRequest request)
    {
        await _usersAppService.ForgetPasswordAsync(request);
        return Ok();
    }

    /// <summary>
    ///     Resets the user's password based on the code sent by e-mail.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] UserResetPasswordRequest request)
    {
        await _usersAppService.ResetPasswordAsync(request);
        return Ok();
    }
}
