using SustentApp.Domain.Utils.Exceptions;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SustentApp.Domain.Users.Entities;

public class User
{
    public string Id { get; protected set; }
    public string Name { get; protected set; }
    public string Document { get; protected set; }
    public Address Address { get; protected set; }
    public string Email { get; protected set; }
    public string Phone { get; protected set; }
    private string Password { get; set; }
    public string Role { get; protected set; }
    public bool ConfirmedEmail { get; protected set; }
    public string SecurityStamp { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected User() { }

    public User(string name, string document, Address address, string email, string phone, string password, string role)
    {
        Id = Guid.NewGuid().ToString();
        SetName(name);
        SetDocument(document);
        SetAddress(address);
        SetEmail(email);
        SetPhone(phone);
        SetPassword(password);
        SetRole(role);
        ConfirmedEmail = false;
        SecurityStamp = Guid.NewGuid().ToString();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new RequiredAttributeException("Name");

        if (name.Length < 3|| name.Length > 100)
            throw new InvalidAttributeLengthException("Name", 3, 100);

        Name = name.Trim();
    }

    public void SetDocument(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new RequiredAttributeException("Document");

        if (document.Length < 11 || document.Length > 14)
            throw new InvalidAttributeLengthException("Document", 11, 14);

        Document = document.Trim();
    }

    public void SetAddress(Address address)
    {
        if (address is null)
            throw new RequiredAttributeException("Address");

        Address = address;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new RequiredAttributeException("Email");

        if (email.Length > 50)
            throw new InvalidAttributeLengthException("Email", 50);

        var mailAddressValidation = MailAddress.TryCreate(email, out var mailAddress);

        if (!mailAddressValidation)
            throw new InvalidAttributeException("Email");

        Email = email.Trim();
    }

    public void SetPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new RequiredAttributeException("Phone");

        phone = phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

        if (phone.Length < 10 || phone.Length > 11)
            throw new InvalidAttributeLengthException("Phone", 10, 11);

        if (!Regex.IsMatch(phone, @"^\d+$"))
            throw new InvalidAttributeException("The phone number must only contain numeric digits or (,) and -.");

        Phone = phone.Trim();
    }

    public void SetPassword(string password) {
        if (string.IsNullOrWhiteSpace(password))
            throw new RequiredAttributeException("Password");

        if (password.Length < 6 || password.Length > 32)
            throw new InvalidAttributeLengthException("Password", 6, 32);

        if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$"))
            throw new InvalidAttributeException("Password must contain at least one uppercase letter, one lowercase letter, one number and one special character.");

        Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        UpdateSecurityStamp();
    }

    public void SetRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new RequiredAttributeException("Role");

        if (role.Length < 3 || role.Length > 20)
            throw new InvalidAttributeLengthException("Role", 3, 20);

        Role = role.Trim();
    }

    public void SetConfirmedEmail(bool confirmedEmail)
    {
        ConfirmedEmail = confirmedEmail;
        UpdateSecurityStamp();
    }

    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.Now;
    }

    public bool CheckPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, Password);
    }

    private void UpdateSecurityStamp()
    {
        SecurityStamp = Guid.NewGuid().ToString();
    }
}
