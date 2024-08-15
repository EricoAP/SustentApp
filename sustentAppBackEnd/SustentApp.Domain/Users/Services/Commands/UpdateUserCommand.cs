namespace SustentApp.Domain.Users.Services.Commands;

public class UpdateUserCommand
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public AddressCommand Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
