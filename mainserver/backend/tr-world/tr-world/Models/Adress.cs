using AltV.Net.Data;

namespace tr_world.Models;

public class Adress
{
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string HousingNumber { get; set; }
    public Position Position { get; set; }
}