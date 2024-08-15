using SustentApp.Domain.Utils.Exceptions;
using System.Text.RegularExpressions;

namespace SustentApp.Domain.Users.Entities;

public class Address
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    protected Address() { }
    public Address(string street, string number, string neighborhood, string city, string state, string zipCode, string complement = null)
    {
        SetStreet(street);
        SetNumber(number);
        SetComplement(complement);
        SetNeighborhood(neighborhood);
        SetCity(city);
        State = state;
        ZipCode = zipCode;
    }

    public void SetStreet(string street)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new RequiredAttributeException("Street");

        if (street.Length < 3 || street.Length > 100)
            throw new InvalidAttributeLengthException("Street", 3, 100);

        Street = street.Trim();
    }

    public void SetNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new RequiredAttributeException("Number");

        if (number.Length > 10)
            throw new InvalidAttributeLengthException("Number", 1, 10);

        Number = number.Trim();
    }

    public void SetComplement(string complement)
    {
        if (string.IsNullOrWhiteSpace(complement))
        {
            Complement = null;
            return;
        }

        if (complement.Length > 100)
            throw new InvalidAttributeLengthException("Complement", 0, 100);

        Complement = complement.Trim();
    }

    public void SetNeighborhood(string neighborhood)
    {
        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new RequiredAttributeException("Neighborhood");

        if (neighborhood.Length < 3 || neighborhood.Length > 100)
            throw new InvalidAttributeLengthException("Neighborhood", 3, 100);

        Neighborhood = neighborhood.Trim();
    }

    public void SetCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new RequiredAttributeException("City");

        if (city.Length < 3 || city.Length > 100)
            throw new InvalidAttributeLengthException("City", 3, 100);

        City = city.Trim();
    }

    public void SetState(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            throw new RequiredAttributeException("State");

        if (state.Length != 2)
            throw new InvalidAttributeLengthException("State", 2, 2);

        State = state.Trim();
    }

    public void SetZipCode(string zipCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new RequiredAttributeException("ZipCode");

        zipCode = zipCode.Replace("-", "");

        if (zipCode.Length != 8)
            throw new InvalidAttributeLengthException("ZipCode", 8, 8);

        if (!Regex.IsMatch(zipCode, @"^\d+$"))
            throw new InvalidAttributeException("The zip code must only contain numeric digits or hyphens.");

        ZipCode = zipCode.Trim();
    }
}
