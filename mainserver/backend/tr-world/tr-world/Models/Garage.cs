namespace tr_world.Models;

public class Garage
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public bool IsForJob => !string.IsNullOrEmpty(JobGarage);
    public string JobGarage { get; set; }
    public TGagVehicleList[] VehicleList { get; set; }

    public void ParkVehicle()
    {
        foreach (var veh in VehicleList)
        {
            
        }
    

    }

    public void UnparkVehicle()
    {
        
    }
}

public class TGagVehicleList
{
    public int VehicleID { get; set; }
    public double VehicleDamage { get; set; }
}