using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Repository
{
    public class DeveloperRepo
    {
        protected readonly List<Developer> _developerDirectory = new List<Developer>();
        private int _id = 1;

        //C
        public bool CreateDeveloper(Developer developer)
        {
            int startingCount = _developerDirectory.Count();
            developer.DevloperId = _id;
            _developerDirectory.Add(developer);
            if (_developerDirectory.Count() > startingCount)
            {
                _id++;
                return true;
            }
            return false;
        }
        //R
        public List<Developer> GetAllDevelopers()
        {
            return _developerDirectory;
        }
        public Developer GetDevById(int id)
        {
            return _developerDirectory.Where(d => d.DevloperId == id).SingleOrDefault();
        }

        public List<Developer> GetByTeamAssignment(SkillSet skills)
        {
            return _developerDirectory.Where(d => d.SkillSet == skills).ToList();
        }
        //U
        public bool UpdateDeveloper(int originalId, Developer newDev)
        {
            Developer oldDev = GetDevById(originalId);
            if (oldDev != null)
            {
                if (newDev.FirstName != null)
                    oldDev.FirstName = newDev.FirstName;
                if (newDev.LastName != null)
                    oldDev.LastName = newDev.LastName;
                oldDev.HasPluralsight = newDev.HasPluralsight;
                oldDev.SkillSet = newDev.SkillSet;

                return true;
            }
            else
            {
                return false;
            }
        }

        //D
        public bool DeleteDeveloper(Developer existingDevloper)
        {
            bool deleteResult = _developerDirectory.Remove(existingDevloper);
            return deleteResult;
        }
    }
}
