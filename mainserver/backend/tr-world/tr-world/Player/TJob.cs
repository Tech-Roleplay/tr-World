using System;
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
        public uint GradeLevel { get; set; }
        public string GradeName { get; set; }
        public string GradeLabel { get; set; }
        public string SkinMale { get; set; } // Change to skin_Model!
        public string SkinFemale { get; set; }
    }

    public class TJob : IJob
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool IsBoss { get; set; }
        public uint GradeLevel { get; set; }
        public string GradeName { get; set; }
        public string GradeLabel { get; set; }
        public bool OnDuty { get; set; }
        public string? Unit { get; set; }
        public uint Payment { get; set; }
        string IJob.SkinMale { get; set; }
        string IJob.SkinFemale { get; set; }



        public TJob()
        {
            Type = "none";
            Name = "unemployed";
            Label = "Civilian";
            IsBoss = false;
            GradeLevel = 0;
            GradeName = "unemployed";
            GradeLabel = "Freelancer";
            OnDuty = true;
            Payment = 800;
        }
    }
}
