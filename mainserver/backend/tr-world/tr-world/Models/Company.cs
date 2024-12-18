namespace tr_world.Models;

public class Company
{
    public string Name { get; set; }
    public string Label { get; set; }
    public string Type { get; set; }
    public ulong Owner { get; set; }
    public string Money { get; set; }
    public double StocksValue { get; set; }
    public string Adress { get; set; }
    public TCpMetaData MetaData { get; set; }
}

public interface ICpMetaData
{
}

public class TCpMetaData : ICpMetaData
{
}