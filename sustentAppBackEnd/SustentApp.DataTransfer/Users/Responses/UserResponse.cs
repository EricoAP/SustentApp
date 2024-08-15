namespace SustentApp.DataTransfer.Users.Responses;

public class UserResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public AddressResponse Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
}
