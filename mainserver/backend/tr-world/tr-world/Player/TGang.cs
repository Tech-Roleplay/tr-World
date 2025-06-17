namespace trWorld.Player;

public interface IGang
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public bool IsBoss { get; set; }
    public bool OnDuty { get; set; }
    public uint GradeLevel { get; set; }
    public string GradeName { get; set; }
    public string GradeLabel { get; set; }
    public string SkinMale { get; set; } // Change to skin_Model!
    public string SkinFemale { get; set; }
}

public class TGang : IGang
{
    public TGang()
    {
        Type = "";
        Name = "";
        Label = "";
        IsBoss = false;
        GradeLevel = 0;
        GradeName = "";
        OnDuty = true;
    }

    public string Type { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public bool IsBoss { get; set; }
    public uint GradeLevel { get; set; }
    public string GradeName { get; set; }
    public string GradeLabel { get; set; }
    public bool OnDuty { get; set; }
    public string SkinMale { get; set; } // Change to skin_Model!
    public string SkinFemale { get; set; }
}