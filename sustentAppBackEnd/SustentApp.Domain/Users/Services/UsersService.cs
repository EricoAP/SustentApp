using OtpNet;
using SustentApp.Domain.Emails.Services.Abstractions;
using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Users.Repositories.Abstractions;
using SustentApp.Domain.Users.Services.Abstractions;
using SustentApp.Domain.Users.Services.Commands;
using SustentApp.Domain.Utils.Exceptions;
using System.Text;

namespace SustentApp.Domain.Users.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailsService _emailsService;
    private readonly ITokenService _tokenService;
    private readonly string _emailConfirmationSecurityStamp = "{0}-email-confirmation";
    private readonly string _passwordResetSecurityStamp = "{0}-password-reset";

    public UsersService(IUsersRepository usersRepository, IEmailsService emailsService, ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _emailsService = emailsService;
        _tokenService = tokenService;
    }

    public async Task<User> CreateAsync(CreateUserCommand command)
    {
        Address address = InstantiateAddress(command.Address);
        User user = Instantiate(command, address);

        await _usersRepository.AddAsync(user);
        //await SendEmailConfirmationAsync(user);
        return user;
    }

    public User GetById(string id)
    {
        User user = _usersRepository.Get(id) ?? throw new RecordNotFoundException("User");
        return user;
    }

    public async Task<User> UpdateAsync(UpdateUserCommand command)
    {
        User user = GetById(command.Id);
        Address address = InstantiateAddress(command.Address);
        string oldEmail = user.Email;

        user.SetName(command.Name);
        user.SetDocument(command.Document);
        user.SetAddress(address);
        user.SetEmail(command.Email);
        user.SetPhone(command.Phone);
        user.SetPassword(command.Password);
        user.SetRole(command.Role);
        user.SetUpdatedAt();

        if (oldEmail != user.Email)
            //await SendEmailConfirmationAsync(user);

        _usersRepository.Update(user);
        return user;
    }

    public Authentication SignIn(string email, string password)
    {
        User user = _usersRepository.Get(u => u.Email == email);

        if (user is null || !user.CheckPassword(password))
            throw new DomainException("Usuário ou senha inválido(s).");

        return new Authentication()
        {
            Token = _tokenService.GenerateToken(user),
            Id = user.Id
        };
    }

    public void ConfirmEmail(string email, string code)
    {
        User user = _usersRepository.Get(u => u.Email == email);

        if (user is null || !CheckEmailConfirmationCode(user, code))
            throw new DomainException("Não foi possível confirmar o e-mail. Por favor, verifique se o e-mail está correto ou se o código já não expirou.");

        user.SetConfirmedEmail(true);
        user.SetUpdatedAt();

        _usersRepository.Update(user);
    }

    public async Task ForgetPasswordAsync(string email)
    {
        User user = _usersRepository.Get(u => u.Email == email);

        if (user is null) return;

        await SendPasswordResetEmailAsync(user);
    }

    public void ResetPassword(string email, string code, string password)
    {
        User user = _usersRepository.Get(u => u.Email == email);

        if (user is null || !CheckPasswordResetCode(user, code))
            throw new DomainException("Não foi possível realizar a redefinição da sua senha. Verifique os dados informados e tente novamente.");

        user.SetPassword(password);
        user.SetUpdatedAt();

        _usersRepository.Update(user);
    }

    private Address InstantiateAddress(AddressCommand command)
    {
        return new(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.ZipCode,
            command.Complement
        );
    }   

    private User Instantiate(CreateUserCommand command, Address address)
    {
        return new(
                    command.Name,
                    command.Document,
                    address,
                    command.Email,
                    command.Phone,
                    command.Password,
                    command.Role);
    }

    private async Task SendEmailConfirmationAsync(User user)
    {
        var data = new
        {
            username = user.Name,
            confirmationCode = GenerateEmailConfirmationCode(user),
            link = "https://localhost"
        };

        await _emailsService.SendEmailAsync(
            to: user.Email, 
            subject: "Bem-vindo ao SustentApp! Confirme seu e-mail.",
            templateFile: "SustentApp.Domain.Users.Templates.email_confirmation.html", 
            data: data, 
            isHtml: true
        );
    }

    private string GenerateEmailConfirmationCode(User user)
    {
        var securityStamp = string.Format(_emailConfirmationSecurityStamp, user.SecurityStamp);
        var secret = Encoding.UTF8.GetBytes(securityStamp);
        var totp = new Totp(secret, step: 3600, totpSize: 8);

        return totp.ComputeTotp();
    }

    private bool CheckEmailConfirmationCode(User user, string code)
    {
        var securityStamp = string.Format(_emailConfirmationSecurityStamp, user.SecurityStamp);
        var secret = Encoding.UTF8.GetBytes(securityStamp);
        var totp = new Totp(secret, step: 3600, totpSize: 8);
        return totp.VerifyTotp(code, out _);
    }

    private async Task SendPasswordResetEmailAsync(User user)
    {
        var data = new
        {
            username = user.Name,
            resetCode = GeneratePasswordResetCodeAsync(user),
            link = "https://localhost"
        };

        await _emailsService.SendEmailAsync(
            to: user.Email,
            subject: "Esqueceu sua senha?",
            templateFile: "SustentApp.Domain.Users.Templates.forget_password.html",
            data: data,
            isHtml: true
        );
    }

    private string GeneratePasswordResetCodeAsync(User user)
    {
        var securityStamp = string.Format(_passwordResetSecurityStamp, user.SecurityStamp);
        var secret = Encoding.UTF8.GetBytes(securityStamp);
        var totp = new Totp(secret, step: 3600, totpSize: 8);

        return totp.ComputeTotp();
    }

    private bool CheckPasswordResetCode(User user, string code)
    {
        var securityStamp = string.Format(_passwordResetSecurityStamp, user.SecurityStamp);
        var secret = Encoding.UTF8.GetBytes(securityStamp);
        var totp = new Totp(secret, step: 3600, totpSize: 8);
        return totp.VerifyTotp(code, out _);
    }
}
