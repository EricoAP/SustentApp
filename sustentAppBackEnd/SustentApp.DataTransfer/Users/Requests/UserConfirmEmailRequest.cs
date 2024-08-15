namespace SustentApp.DataTransfer.Users.Requests;

public class UserConfirmEmailRequest
{
    public string Email { get; set; }
    public string Code { get; set; }
}
