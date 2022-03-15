using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Repository
{
    public enum SkillSet { FrontEnd=1, BackEnd, Testing}
    public class Developer
    {
        public Developer() { }
        public Developer(string first,string last, SkillSet skills, bool hasPs)
        {
            FirstName = first;
            LastName = last;
            SkillSet = skills;
            HasPluralsight = hasPs;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int DevloperId { get; set; }
        public SkillSet SkillSet { get; set; }
        public bool HasPluralsight { get; set; }
    }
}
