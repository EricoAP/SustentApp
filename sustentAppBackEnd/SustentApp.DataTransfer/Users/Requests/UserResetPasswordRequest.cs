namespace SustentApp.DataTransfer.Users.Requests;

public class UserResetPasswordRequest
{
    public string Email { get; set; }
    public string Code { get; set; }
    public string Password { get; set; }
}
