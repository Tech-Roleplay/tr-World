using AltV.Net.Data;

namespace tr_world.Models;

/// <summary>
/// Represents an address with properties for the city, country, street, postal code, housing number, and geographic position.
/// </summary>
public class Adress
{
    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the country associated with the address.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the street name of the address.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the address.
    /// Represents the code used to identify a specific geographic area for mail delivery.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// Represents the housing number in the address, typically associated with a specific building or unit.
    /// </summary>
    public string HousingNumber { get; set; }

    /// <summary>
    /// Represents the server coordinates of the address, including latitude, longitude, and altitude.
    /// </summary>
    public Position Position { get; set; }
}