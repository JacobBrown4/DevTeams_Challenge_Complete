using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Repository
{
    public class DevTeam
    {
        public DevTeam() { }
        public DevTeam(string name)
        {
            TeamName = name;
        }
        public int TeamdId { get; set; }
        public List<Developer> Developers { get; set; } = new List<Developer>();
        public string TeamName { get; set; }
    }
}
