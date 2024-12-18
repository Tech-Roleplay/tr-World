using System;

namespace tr_world.Player;

public interface IMetadata
{
    //bools
    public bool IsCuffed { get; set; }
    public bool IsInPrison { get; set; }
    public bool IsPlyDead { get; set; }
    public bool IsPlyDown { get; set; }
    public bool IsPlyHeadshotted { get; set; }
    public bool IsPlyLogout { get; set; }

    //strings 

    // ints

    public float Hunger { get; set; }
    public float Thirst { get; set; }
    public float Armor { get; set; }
    public float Stress { get; set; }
    public float JailTime { get; set; }


    public DateTime LastUpdate { get; set; }
    public DateTime CreateDate { get; set; }
}

public class TMetadata : IMetadata
{
    public TMetadata()
    {
        IsCuffed = false;
        IsInPrison = false;
        IsPlyDead = false;
        IsPlyDown = false;
        IsPlyHeadshotted = false;
        IsPlyLogout = false;
        Hunger = 100;
        Thirst = 100;
        Armor = 100;
        Stress = 100;
        JailTime = 0;
        LastUpdate = DateTime.Now;
        CreateDate = DateTime.Now - TimeSpan.FromSeconds(1);
    }

    public bool IsCuffed { get; set; }
    public bool IsInPrison { get; set; }
    public bool IsPlyDead { get; set; }
    public bool IsPlyDown { get; set; }
    public bool IsPlyHeadshotted { get; set; }
    public bool IsPlyLogout { get; set; }
    public float Hunger { get; set; }
    public float Thirst { get; set; }
    public float Armor { get; set; }
    public float Stress { get; set; }
    public float JailTime { get; set; }
    public DateTime LastUpdate { get; set; }
    public DateTime CreateDate { get; set; }
}