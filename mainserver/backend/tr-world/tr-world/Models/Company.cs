namespace tr_world.Models;

/// <summary>
/// Represents a company entity with attributes such as name, label, type,
/// owner identifier, financial information, address, and metadata details.
/// </summary>
public class Company
{
    /// <summary>
    /// Gets or sets the name of the company.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Represents the label associated with the company.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Gets or sets the type of the company. This property represents
    /// a classification or category associated with the company.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Represents the unique identifier of the owner associated with the company.
    /// </summary>
    public ulong Owner { get; set; }

    /// <summary>
    /// Represents the financial resources or monetary value associated with the company.
    /// </summary>
    public string Money { get; set; }

    /// <summary>
    /// Represents the current value of the company's stocks.
    /// This property stores the monetary worth of all shares within the company,
    /// which is typically used for financial evaluations or investment decisions.
    /// </summary>
    public double StocksValue { get; set; }

    /// <summary>
    /// Gets or sets the address associated with the company.
    /// </summary>
    /// <remarks>
    /// This property represents the location or address details related to the company.
    /// </remarks>
    public string Adress { get; set; }

    /// <summary>
    /// Gets or sets the metadata associated with the company.
    /// </summary>
    /// <remarks>
    /// This property holds additional metadata information for a company instance.
    /// It can be used to store auxiliary data or extend the functionality of the company object.
    /// The metadata is represented using the <see cref="TCpMetaData"/> class which implements the <see cref="ICpMetaData"/> interface.
    /// </remarks>
    public TCpMetaData MetaData { get; set; }
}

/// <summary>
/// Represents metadata information for the Company entity.
/// </summary>
public interface ICpMetaData
{
}

/// <summary>
/// Represents meta-information associated with a company.
/// </summary>
/// <remarks>
/// This class implements the <see cref="ICpMetaData"/> interface to hold metadata details for a company object.
/// It is primarily used within the `Company` model and provides structure for additional company-specific metadata.
/// Extend this class to include more metadata as needed.
/// </remarks>
public class TCpMetaData : ICpMetaData
{
}