using DevTeams_Challenge_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevTeams_Tests
{
    [TestClass]
    public class DevTeamsRepo_Tests
    {
        DevTeamsRepo _repo;
        [TestInitialize]
        public void Initial()
        {
            _repo = new DevTeamsRepo();
            Developer dev1 = new Developer("Richard", "Hendricks", SkillSet.BackEnd, true);
            Developer dev2 = new Developer("Bertram", "Gilfoyle", SkillSet.BackEnd, false);
            Developer dev3 = new Developer("Jared","Dunn",SkillSet.Testing,true);
            DevTeam devT = new DevTeam("Pied Piper");
            _repo.CreateDeveloper(dev1);
            _repo.CreateDeveloper(dev2);
            _repo.CreateDeveloper(dev3);
            devT.Developers.Add(dev1);
            devT.Developers.Add(dev2);
            devT.Developers.Add(dev3);
            _repo.CreateDevTeam(devT);
        }
        [TestMethod]
        public void CreateDeveloper_ShouldAddDeveloperToDirectory()
        {
            Developer dev = new Developer("Dinesh", "Chugtai", SkillSet.Testing, true);
            bool result = _repo.CreateDeveloper(dev);
            Assert.IsTrue(result);
            Assert.IsTrue(_repo.GetAllDevelopers().Contains(dev));
        }
        [TestMethod]
        public void UpdateDevloper_ShouldChangeDeveloperInformation()
        {
            string previousName = _repo.GetDevById(3).FirstName;
            bool result = _repo.UpdateDeveloper(3, new Developer("Donald", "Dunn", SkillSet.Testing, true));
            Assert.IsTrue(result);
            var dev = _repo.GetDevById(3);
            Assert.AreNotEqual(previousName, dev.FirstName);
            Assert.AreEqual("Donald", dev.FirstName);
        }
        [TestMethod]
        public void DeleteDeveloper_ShouldRemoveDevloper()
        {
            Developer dev = new Developer("Dinesh", "Chugtai", SkillSet.Testing, true);
            _repo.CreateDeveloper(dev);
            Assert.IsTrue(_repo.DeleteDeveloper(dev));
            Assert.IsFalse(_repo.GetAllDevelopers().Contains(dev));
        }

        // Dev teams test

        [TestMethod]
        public void CreateDevTeam_ShouldAddTeamToDirectory()
        {
            DevTeam devT = new DevTeam("Test Team");
            Assert.IsTrue(_repo.CreateDevTeam(devT));
            Assert.IsTrue(_repo.GetAllDevTeams().Contains(devT));
        }
        [TestMethod]
        public void UpdateDevTeam_ShouldChangeTeamName()
        {
            string previousName = _repo.GetDevTeamById(1).TeamName;
            bool result = _repo.UpdateDevTeam(1, new DevTeam("Hooli"));
            Assert.IsTrue(result);
            var dev = _repo.GetDevTeamById(1);
            Assert.AreNotEqual(previousName, dev.TeamName);
            Assert.AreEqual("Hooli", dev.TeamName);
        }
        [TestMethod]
        public void DeleteDevTeam_ShouldRemoveDevTeam()
        {
            DevTeam devT = new DevTeam("Test Team");
            _repo.CreateDevTeam(devT);
            Assert.IsTrue(_repo.DeleteDevTeam(devT));
            Assert.IsFalse(_repo.GetAllDevTeams().Contains(devT));

        }
    }
}
