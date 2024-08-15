namespace SustentApp.DataTransfer.Users.Requests;

public class UserUpdateRequest
{
    public string Name { get; set; }
    public string Document { get; set; }
    public AddressRequest Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
