using FizzWare.NBuilder;
using FluentAssertions;
using SustentApp.Domain.Users.Entities;
using SustentApp.Domain.Utils.Exceptions;

namespace SustentApp.Domain.Tests.Users.Entities;

public class AddressTests
{
    private readonly Address sut;

    public AddressTests()
    {
        sut = Builder<Address>.CreateNew().Build();
    }

    public class Constructor
    {
        [Fact]
        public void When_ValidParametersAreGiven_Then_InstantiateNewAddress()
        {
            const string STREET = "Rua dos Testes Unitários";
            const string NUMBER = "123";
            const string COMPLEMENT = "Ed. CSharp";
            const string NEIGHBORHOOD = "Lapa";
            const string CITY = "Rio de Janeiro";
            const string STATE = "RJ";
            const string ZIPCODE = "12345678";

            var address = new Address(STREET, NUMBER, NEIGHBORHOOD, CITY, STATE, ZIPCODE, COMPLEMENT);

            address.Street.Should().Be(STREET);
            address.Number.Should().Be(NUMBER);
            address.Complement.Should().Be(COMPLEMENT);
            address.Neighborhood.Should().Be(NEIGHBORHOOD);
            address.City.Should().Be(CITY);
            address.State.Should().Be(STATE);
            address.ZipCode.Should().Be(ZIPCODE);
        }
    }

    public class SetStreetMethod : AddressTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_StreetIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string street)
        {
            sut.Invoking(x => x.SetStreet(street)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(101)]
        public void When_StreetLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var street = new string('A', length);

            sut.Invoking(x => x.SetStreet(street)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidStreetIsGiven_Then_SetStreet()
        {
            const string STREET = "Rua dos Testes Unitários";

            sut.SetStreet(STREET);

            sut.Street.Should().Be(STREET);
        }
    }

    public class SetNumberMethod : AddressTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_NumberIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string number)
        {
            sut.Invoking(x => x.SetNumber(number)).Should().Throw<RequiredAttributeException>();
        }

        [Fact]
        public void When_NumberLengthIsInvalid_Then_ThrowInvalidAttributeLengthException()
        {
            const int MAX_LENGTH = 10;
            var number = new string('1', MAX_LENGTH + 1);

            sut.Invoking(x => x.SetNumber(number)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidNumberIsGiven_Then_SetNumber()
        {
            const string NUMBER = "123";

            sut.SetNumber(NUMBER);

            sut.Number.Should().Be(NUMBER);
        }
    }

    public class SetComplementMethod : AddressTests
    {
       [Fact]
        public void When_ComplementLengthIsInvalid_Then_ThrowInvalidAttributeLengthException()
        {
            const int MAX_LENGTH = 100;
            var complement = new string('A', MAX_LENGTH + 1);

            sut.Invoking(x => x.SetComplement(complement)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_ComplementIsNullOrWhiteSpace_Then_SetComplementToNull(string complement)
        {
            sut.SetComplement(complement);

            sut.Complement.Should().BeNull();
        }

        [Fact]
        public void When_ValidComplementIsGiven_Then_SetComplement()
        {
            const string COMPLEMENT = "Ed. CSharp";

            sut.SetComplement(COMPLEMENT);

            sut.Complement.Should().Be(COMPLEMENT);
        }
    }

    public class SetNeighborhoodMethod : AddressTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_NeighborhoodIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string neighborhood)
        {
            sut.Invoking(x => x.SetNeighborhood(neighborhood)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(101)]
        public void When_NeighborhoodLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var neighborhood = new string('A', length);

            sut.Invoking(x => x.SetNeighborhood(neighborhood)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidNeighborhoodIsGiven_Then_SetNeighborhood()
        {
            const string NEIGHBORHOOD = "Lapa";

            sut.SetNeighborhood(NEIGHBORHOOD);

            sut.Neighborhood.Should().Be(NEIGHBORHOOD);
        }
    }

    public class SetCityMethod : AddressTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_CityIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string city)
        {
            sut.Invoking(x => x.SetCity(city)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(101)]
        public void When_CityLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var city = new string('A', length);

            sut.Invoking(x => x.SetCity(city)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidCityIsGiven_Then_SetCity()
        {
            const string CITY = "Rio de Janeiro";

            sut.SetCity(CITY);

            sut.City.Should().Be(CITY);
        }
    }

    public class SetStateMethod : AddressTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_StateIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string state)
        {
            sut.Invoking(x => x.SetState(state)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void When_StateLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var state = new string('A', length);

            sut.Invoking(x => x.SetState(state)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ValidStateIsGiven_Then_SetState()
        {
            const string STATE = "RJ";

            sut.SetState(STATE);

            sut.State.Should().Be(STATE);
        }
    }

    public class SetZipCodeMethod : AddressTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void When_ZipCodeIsNullOrWhiteSpace_Then_ThrowRequiredAttributeException(string zipCode)
        {
            sut.Invoking(x => x.SetZipCode(zipCode)).Should().Throw<RequiredAttributeException>();
        }

        [Theory]
        [InlineData(7)]
        [InlineData(9)]
        public void When_ZipCodeLengthIsInvalid_Then_ThrowInvalidAttributeLengthException(int length)
        {
            var zipCode = new string('1', length);

            sut.Invoking(x => x.SetZipCode(zipCode)).Should().Throw<InvalidAttributeLengthException>();
        }

        [Fact]
        public void When_ZipCodeDoesntContainsOnlyNumbers_Then_ThrowInvalidAttributeException()
        {
            const string ZIPCODE = "1234567A";

            sut.Invoking(x => x.SetZipCode(ZIPCODE)).Should().Throw<InvalidAttributeException>();
        }

        [Fact]
        public void When_ValidZipCodeIsGiven_Then_SetZipCode()
        {
            const string ZIPCODE = "12345678";

            sut.SetZipCode(ZIPCODE);

            sut.ZipCode.Should().Be(ZIPCODE);
        }
    }
}
