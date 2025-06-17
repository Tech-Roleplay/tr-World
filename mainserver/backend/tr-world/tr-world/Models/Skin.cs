namespace trWorld.Models;

public class Skin
{
    public ComponentIDs componentIDs { get; set; }
}

public enum ComponentIDs
{
    Head = 0,
    Mask,
    HairStye,
    Torso,
    Legs,
    Bags,
    Shoes,
    Accessories,
    Undershirts,
    BodyArmour,
    Decals,
    Tops
}