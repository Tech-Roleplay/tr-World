﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_world.Player
{
    public interface IJob
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public uint Payment { get; set; }
        public bool IsBoss { get; set; }
        public bool OnDuty { get; set; }
        public string? Unit {  get; set; }
        public uint Grade_level { get; set; }
        public string Grade_name { get; set; }
        public string Grade_Label { get; set; }
        public string Skin_Male { get; set; } // Change to skin_Model!
        public string Skin_Female { get; set; }
    }

    public class TJob : IJob
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool IsBoss { get; set; }
        public uint Grade_level { get; set; }
        public string Grade_name { get; set; }
        public string Grade_Label { get; set; }
        public bool OnDuty { get; set; }
        public string? Unit { get; set; }
        public uint Payment { get; set; }
        string IJob.Skin_Male { get; set; }
        string IJob.Skin_Female { get; set; }



        public TJob()
        {
            Type = "none";
            Name = "unemployed";
            Label = "Civilian";
            IsBoss = false;
            Grade_level = 0;
            Grade_name = "unemployed";
            Grade_Label = "Freelancer";
            OnDuty = true;
            Payment = 800;
        }
    }
}
