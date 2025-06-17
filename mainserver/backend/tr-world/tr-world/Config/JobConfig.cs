namespace trWorld.Config;

public class JobConfig
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public bool is_Required_to_be_invited { get; set; }
    public bool whitelist { get; set; }
    public JobGradesConfig Grade { get; set; }
}

/*
 * Erstelle eine Liste wennn der Code gephrased wird
 *
List<JobConfig> jobs = new List<JobConfig>
{
new JobConfig
{
    Type = "Police",
    // ... other properties
},
new JobConfig
{
    Type = "Firefighter",
    // ... other properties
}
};
 *
 */
public class JobGradesConfig
{
    public int Level { get; set; }
    public string Name { set; get; }
    public string Label { set; get; }
    public bool IsBoss { get; set; }
    public bool OnDuty { get; set; }
    public uint Salary { get; set; }
    public string Skin { get; set; }
}