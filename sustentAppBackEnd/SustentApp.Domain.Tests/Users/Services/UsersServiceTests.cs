using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SustentApp.Domain.Emails.Services.Abstractions;
using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Users.Repositories.Abstractions;
using SustentApp.Domain.Users.Services;
using SustentApp.Domain.Users.Services.Abstractions;
using SustentApp.Domain.Users.Services.Commands;
using SustentApp.Domain.Utils.Exceptions;

namespace SustentApp.Domain.Tests.Users.Services;

public class UsersServiceTests
{
    private readonly UsersService sut;
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailsService _emailsService;
    private readonly ITokenService _tokenService;
    private readonly User _validUser;

    public UsersServiceTests()
    {
        _usersRepository = Substitute.For<IUsersRepository>();
        _emailsService = Substitute.For<IEmailsService>();
        _tokenService = Substitute.For<ITokenService>();
        sut = new UsersService(_usersRepository, _emailsService, _tokenService);
        _validUser = Builder<User>.CreateNew()
                .With(u => u.Email, "unit@tests.com")
                .Build();
    }

    public class CreateMethod : UsersServiceTests
    {
        [Fact]
        public async Task When_ValidParametersAreGiven_Then_ShouldCreateUserAsync()
        {
            CreateUserCommand command = new()
            {
                Name = "John Doe",
                Document = "12345678900",
                Address = new AddressCommand
                {
                    Street = "Rua dos Testes Unitários",
                    Number = "123",
                    Complement = "Ed. CSharp",
                    Neighborhood = "Lapa",
                    City = "Rio de Janeiro",
                    State = "RJ",
                    ZipCode = "12345678"
                },
                Email = "unit@tests.com",
                Phone = "21999999999",
                Password = "Test@2024",
                Role = "User"
            };

            User user = await sut.CreateAsync(command);

            await _usersRepository.Received(1).AddAsync(user);
            await _emailsService.Received(1).SendEmailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<object>(), Arg.Any<bool>());
        }
    }

    public class GetByIdMethod : UsersServiceTests
    {
        [Fact]
        public void When_UserNotFound_Then_ThrowRecordNotFoundException()
        {
            _usersRepository.Get(Arg.Any<string>()).ReturnsNull();

            sut.Invoking(x => x.GetById("123"))
                .Should()
                .Throw<RecordNotFoundException>();
        }

        [Fact]
        public void When_UserExists_Then_ShouldReturnUser()
        {
            _usersRepository.Get(_validUser.Id).Returns(_validUser);

            User user = sut.GetById(_validUser.Id);

            user.Should().Be(_validUser);
        }
    }

    public class UpdateMethod : UsersServiceTests
    {
        [Fact]
        public async Task When_ValidParametersAreGiven_Then_UserUpdatedAsync()
        {
            _usersRepository.Get(Arg.Any<string>()).Returns(_validUser);

            UpdateUserCommand command = new()
            {
                Id = "123",
                Name = "John Doe",
                Document = "12345678900",
                Address = new AddressCommand
                {
                    Street = "Rua dos Testes Unitários",
                    Number = "123",
                    Complement = "Ed. CSharp",
                    Neighborhood = "Lapa",
                    City = "Rio de Janeiro",
                    State = "RJ",
                    ZipCode = "12345678"
                },
                Email = "unit@tests.com",
                Phone = "21999999999",
                Password = "Test@2024",
                Role = "User"
            };

            User user = await sut.UpdateAsync(command);

            user.Name.Should().Be(command.Name);
            user.Document.Should().Be(command.Document);
            user.Address.Street.Should().Be(command.Address.Street);
            user.Address.Number.Should().Be(command.Address.Number);
            user.Address.Complement.Should().Be(command.Address.Complement);
            user.Address.Neighborhood.Should().Be(command.Address.Neighborhood);
            user.Address.City.Should().Be(command.Address.City);
            user.Address.State.Should().Be(command.Address.State);
            user.Address.ZipCode.Should().Be(command.Address.ZipCode);
            user.Email.Should().Be(command.Email);
            user.Phone.Should().Be(command.Phone);
            user.CheckPassword(command.Password).Should().BeTrue();
            user.Role.Should().Be(command.Role);
            user.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            _usersRepository.Received(1).Update(user);
        }

        [Fact]
        public async Task When_EmailsAreDifferent_Then_SendEmailConfirmationAsync()
        {
            _usersRepository.Get(Arg.Any<string>()).Returns(_validUser);

            UpdateUserCommand command = new()
            {
                Id = "123",
                Name = "John Doe",
                Document = "12345678900",
                Address = new AddressCommand
                {
                    Street = "Rua dos Testes Unitários",
                    Number = "123",
                    Complement = "Ed. CSharp",
                    Neighborhood = "Lapa",
                    City = "Rio de Janeiro",
                    State = "RJ",
                    ZipCode = "12345678"
                },
                Email = "unitDiff@tests.com",
                Phone = "21999999999",
                Password = "Test@2024",
                Role = "User"
            };

            User user = await sut.UpdateAsync(command);

            user.Name.Should().Be(command.Name);
            user.Document.Should().Be(command.Document);
            user.Address.Street.Should().Be(command.Address.Street);
            user.Address.Number.Should().Be(command.Address.Number);
            user.Address.Complement.Should().Be(command.Address.Complement);
            user.Address.Neighborhood.Should().Be(command.Address.Neighborhood);
            user.Address.City.Should().Be(command.Address.City);
            user.Address.State.Should().Be(command.Address.State);
            user.Address.ZipCode.Should().Be(command.Address.ZipCode);
            user.Email.Should().Be(command.Email);
            user.Phone.Should().Be(command.Phone);
            user.CheckPassword(command.Password).Should().BeTrue();
            user.Role.Should().Be(command.Role);
            user.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            _usersRepository.Received(1).Update(user);
            await _emailsService.Received(1).SendEmailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<object>(), Arg.Any<bool>());
        }
    }
}
