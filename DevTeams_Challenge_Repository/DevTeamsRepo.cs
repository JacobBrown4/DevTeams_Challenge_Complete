using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Repository
{
    public class DevTeamsRepo : DeveloperRepo
    {
        //This is our Repository class that will hold our directory (which will act as our database) and methods that will directly talk to our directory.

        protected List<DevTeam> _devTeamDirectory = new List<DevTeam>();
        private int _id = 1;

        // C
        public bool CreateDevTeam(DevTeam devT)
        {
            int startingCount = _devTeamDirectory.Count();
            devT.TeamdId = _id;
            _devTeamDirectory.Add(devT);
            if (_devTeamDirectory.Count() > startingCount)
            {
                _id++;
                return true;
            }
            return false;
        }

        public bool AddDevloperToTeamById(int devId, int teamId)
        {
            Developer dev = GetDevById(devId);

            DevTeam dTeam = GetDevTeamById(teamId);
            if (dev != default && dTeam != default)
            {
                if (!dTeam.Developers.Contains(dev))
                {
                    int startingCount = dTeam.Developers.Count();
                    dTeam.Developers.Add(dev);
                    return dTeam.Developers.Count > startingCount ? true : false;
                }
            }
            return false;

        }
        // R
        public DevTeam GetDevTeamById(int teamId)
        {
            return _devTeamDirectory.Where(d => d.TeamdId == teamId).SingleOrDefault();
        }
        public List<DevTeam> GetAllDevTeams()
        {
            return _devTeamDirectory;
        }

        // U
        public bool RemoveDeveloperFromTeamById(int devId, int teamId)
        {
            DevTeam dTeam = GetDevTeamById(teamId);
            Developer dev = dTeam.Developers.Where(d => d.DevloperId == devId).SingleOrDefault();
            if (dev != default)
            {
                return dTeam.Developers.Remove(dev);
            }
            else
                return false;
        }

        public bool UpdateDevTeam(int id, DevTeam newTeam)
        {
            DevTeam oldTeam = GetDevTeamById(id);
            if (oldTeam != null)
            {
                oldTeam.TeamName = newTeam.TeamName;
                if (newTeam.Developers.Count > 0)
                {
                    oldTeam.Developers = newTeam.Developers;
                }
                return true;
            }
            return false;
        }
        // D
        public bool DeleteDevTeam(DevTeam devT)
        {
            return _devTeamDirectory.Remove(devT);
        }
    }
}
