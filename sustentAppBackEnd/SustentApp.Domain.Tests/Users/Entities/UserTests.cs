using FizzWare.NBuilder;
using FluentAssertions;
using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Utils.Exceptions;

namespace SustentApp.Domain.Tests.Users.Entities;

public class UserTests
{
    private readonly User sut;

    public UserTests()
    {
        sut = Builder<User>.CreateNew().Build();
    }

    public class Constructor
    {
        [Fact]
        public void When_ValidParametersAreGiven_Then_InstantiateNewUser()
        {
            const string NAME = "John Doe";
            const string DOCUMENT = "12345678901";
            var address = Builder<Address>.CreateNew().Build();
            const string EMAIL = "unit@tests.com";
            const string PHONE = "12345678901";
            const string PASSWORD = "Test@2024";
            const string ROLE = "User";

            var user = new User(NAME, DOCUMENT, address, EMAIL, PHONE, PASSWORD, ROLE);

            user.Id.Should().NotBeNullOrWhiteSpace();
            user.Name.Should().Be(NAME);
            user.Document.Should().Be(DOCUMENT);
            user.Address.Should().Be(address);
            user.Email.Should().Be(EMAIL);
            user.Phone.Should().Be(PHONE);
            user.Role.Should().Be(ROLE);
            user.ConfirmedEmail.Should().BeFalse();
            user.SecurityStamp.Should().NotBeNullOrWhiteSpace();
            user.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            user.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }

    public class SetNameMethod : UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_NameIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string name)
        {
            sut.Invoking(x => x.SetName(name)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(101)]
        public void When_NameLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var name = new string('A', length);

            sut.Invoking(x => x.SetName(name)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidNameIsGiven_Then_SetName()
        {
            const string NAME = "John Doe";

            sut.SetName(NAME);

            sut.Name.Should().Be(NAME);
        }
    }

    public class SetDocumentMethod : UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_DocumentIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string document)
        {
            sut.Invoking(x => x.SetDocument(document)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        public void When_DocumentLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var document = new string('1', length);

            sut.Invoking(x => x.SetDocument(document)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidDocumentIsGiven_Then_SetDocument()
        {
            const string DOCUMENT = "12345678901";

            sut.SetDocument(DOCUMENT);

            sut.Document.Should().Be(DOCUMENT);
        }
    }

    public class SetAddressMethod : UserTests
    {
        [Fact]
        public void When_AddressIsNull_Then_ThrowRequiredAttributeException()
        {
            sut.Invoking(x => x.SetAddress(null)).Should().Throw<RequiredAttributeException>();
        }

        [Fact]
        public void When_ValidAddressIsGiven_Then_SetAddress()
        {
            var address = Builder<Address>.CreateNew().Build();

            sut.SetAddress(address);

            sut.Address.Should().Be(address);
        }
    }

    public class SetEmailMethod : UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_EmailIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string email)
        {
            sut.Invoking(x => x.SetEmail(email)).Should().Throw<RequiredAttributeException>();
        }

        [Fact]
        public void When_EmailLengthIsInvalid_Then_ThrowInvalidAttributeLengthException()
        {
            const int MAX_LENGTH = 50;
            var email = new string('A', MAX_LENGTH + 1);

            sut.Invoking(x => x.SetEmail(email)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Theory]
        [InlineData("invalidemail")]
        [InlineData("invalidemail@")]
        [InlineData("invalidemail.com")]
        public void When_EmailIsInvalid_Then_ThrowInvalidEmailException(string email)
        {
            sut.Invoking(x => x.SetEmail(email)).Should().Throw<InvalidAttributeException>();
        }

        [Fact]
        public void When_ValidEmailIsGiven_Then_SetEmail()
        {
            const string EMAIL = "unit@test.com";

            sut.SetEmail(EMAIL);

            sut.Email.Should().Be(EMAIL);
        }
    }

    public class SetPhoneMethod : UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_PhoneIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string phone)
        {
            sut.Invoking(x => x.SetPhone(phone)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(9)]
        [InlineData(15)]
        public void When_PhoneLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var phone = new string('1', length);

            sut.Invoking(x => x.SetPhone(phone)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_PhoneContainsLetters_Then_ThrowInvalidAttributeException()
        {
            const string PHONE = "1234567890A";

            sut.Invoking(x => x.SetPhone(PHONE)).Should().Throw<InvalidAttributeException>();
        }

        [Fact]
        public void When_ValidPhoneIsGiven_Then_SetPhone()
        {
            const string PHONE = "12345678901";

            sut.SetPhone(PHONE);

            sut.Phone.Should().Be(PHONE);
        }
    }

    public class SetPasswordMethod : UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_PasswordIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string password)
        {
            sut.Invoking(x => x.SetPassword(password)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(33)]
        public void When_PasswordLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var password = new string('A', length);

            sut.Invoking(x => x.SetPassword(password)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Theory]
        [InlineData("invalidpassword")]
        [InlineData("invalidpassword@")]
        [InlineData("invalidpassword2024")]
        public void When_PasswordIsInvalid_Then_ThrowInvalidPasswordException(string password)
        {
            sut.Invoking(x => x.SetPassword(password)).Should().Throw<InvalidAttributeException>();
        }

        [Fact]
        public void When_ValidPasswordIsGiven_Then_SetPassword()
        {
            string oldSecurityStamp = sut.SecurityStamp;
            const string PASSWORD = "Test@2024";

            sut.SetPassword(PASSWORD);

            sut.CheckPassword(PASSWORD).Should().BeTrue();
            sut.SecurityStamp.Should().NotBe(oldSecurityStamp);
            sut.SecurityStamp.Should().NotBeNullOrWhiteSpace();
        }
    }

    public class SetRoleMethod : UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_RoleIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string role)
        {
            sut.Invoking(x => x.SetRole(role)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(21)]
        public void When_RoleLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var role = new string('A', length);

            sut.Invoking(x => x.SetRole(role)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidRoleIsGiven_Then_SetRole()
        {
            const string ROLE = "User";

            sut.SetRole(ROLE);

            sut.Role.Should().Be(ROLE);
        }
    }

    public class SetConfirmedEmailMethod : UserTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void When_ValidParametersAreGiven_Expect_FilledProperty(bool confirmedEmail)
        {
            string oldSecurityStamp = sut.SecurityStamp;

            sut.SetConfirmedEmail(confirmedEmail);

            sut.ConfirmedEmail.Should().Be(confirmedEmail);
            sut.SecurityStamp.Should().NotBe(oldSecurityStamp);
            sut.SecurityStamp.Should().NotBeNullOrWhiteSpace();
        }
    }

    public class CheckPasswordMethod : UserTests
    {
        [Fact]
        public void When_PasswordIsIncorrect_Then_ReturnFalse()
        {
            const string PASSWORD = "Test@2024";
            const string INCORRECT_PASSWORD = "Test@2025";
            sut.SetPassword(PASSWORD);

            var result = sut.CheckPassword(INCORRECT_PASSWORD);

            result.Should().BeFalse();
        }

        [Fact]
        public void When_PasswordIsCorrect_Then_ReturnTrue()
        {
            const string PASSWORD = "Test@2024";
            sut.SetPassword(PASSWORD);

            var result = sut.CheckPassword(PASSWORD);

            result.Should().BeTrue();
        }
    }
}
