using DevTeams_Challenge_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Console
{
    public class ProgramUI
    {
        private DevTeamsRepo _devTeamRepo = new DevTeamsRepo();
        public void Run()
        {
            SeedDevs();
            Menu();
        }
        private void Menu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();

                Console.WriteLine("Enter the number of the option you would like:\n" +
                    "01. Show all Devlopers\n" +
                    "02. Show all Dev Teams\n" +
                    "03. Get Developer Details\n" +
                    "04. Get Team Details\n" +
                    "05. Add Developer to directory\n" +
                    "06. Add Dev Team to directory\n" +
                    "07. Add Developer to Dev Team\n" +
                    "08. Update Developer\n" +
                    "09. Update Dev Team\n" +
                    "10. Delete Developer\n" +
                    "11. Delete Dev Team\n" +
                    "12. Remove Developer From Dev Team\n" +
                    "13. Exit");

                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                    case "show":
                        ShowAllDevs();
                        break;
                    case "2":
                        ShowAllDevTeams();
                        break;
                    case "3":
                        GetDevById();
                        break;
                    case "4":
                        GetDevTeamById();
                        break;
                    case "5":
                        CreateDev();
                        break;
                    case "6":
                        CreateDevTeam();
                        break;
                    case "7":
                        AddDevToTeam();
                        break;
                    case "8":
                        UpdateDev();
                        break;
                    case "9":
                        UpdateDevTeam();
                        break;
                    case "10":
                        DeleteDev();
                        break;
                    case "11":
                        DeleteDevTeam();
                        break;
                    case "12":
                        RemoveDevFromTeam();
                        break;
                    case "13":
                    case "e":
                    case "exit":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number between 1 and 7. \n" +
                            "Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void CreateDev()
        {
            Console.Clear();
            Developer newDev = new Developer();
            Console.Write("First Name: ");
            newDev.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            newDev.LastName = Console.ReadLine();

            Console.WriteLine("Select their skill set:\n" +
                "1. Front End\n" +
                "2. Back End\n" +
                "3. Testing\n");

            newDev.SkillSet = (SkillSet)int.Parse(Console.ReadLine());

            Console.Write("Access to Plural Sight: ");
            switch (Console.ReadLine().ToLower())
            {
                case "true":
                case "yes":
                    newDev.HasPluralsight = true; //false by default
                    break;
            }

            if (_devTeamRepo.CreateDeveloper(newDev))
                Success();
            else
                Failure("");
        }
        private void CreateDevTeam()
        {
            Console.Clear();
            DevTeam team = new DevTeam();
            Console.Write("Team Name: ");
            team.TeamName = Console.ReadLine();

            if (_devTeamRepo.CreateDevTeam(team))
            {
                Success();
                var id = _devTeamRepo.GetAllDevTeams().OrderByDescending(d => d.TeamdId).FirstOrDefault().TeamdId;
                AddDevLoop(id);
            }
            else
                Failure("");
        }
        // Read
        private void ShowAllDevs()
        {
            Console.Clear();
            DisplayAllDevs();
            AnyKey();
        }
        private void ShowAllDevTeams()
        {
            Console.Clear();
            DisplayAllDevTeams();
            AnyKey();
        }
        private void GetDevById()
        {
            Console.Clear();
            DisplayAllDevs();
            Console.Write("Enter a Dev Id: ");
            string id = Console.ReadLine();
            Developer dev = _devTeamRepo.GetDevById(Int32.Parse(id));
            if (dev != null)
            {
                Console.WriteLine();
                DisplayDeveloper(dev);
                AnyKey();
            }
            else
            {
                Failure("Couldn't find a developer by that title.");
            }
        }
        private void GetDevTeamById()
        {
            Console.Clear();
            Console.Write("Enter Team Id: ");
            DevTeam team = _devTeamRepo.GetDevTeamById(Int32.Parse(Console.ReadLine()));
            if (team != null)
            {
                DisplayDevTeam(team);
                Console.WriteLine("\nDevelopers:\n");
                foreach (var dev in team.Developers)
                {
                    Console.WriteLine();
                    DisplayDeveloper(dev);
                }
                AnyKey();
            }
            else
                Failure("Team not found by that id.");
        }
        // Update
        private void UpdateDev()
        {
            Console.Clear();
            Console.WriteLine("Update Devloper");
            DisplayAllDevs();
            Console.Write("Enter a Dev Id: ");
            var id = Int32.Parse(Console.ReadLine());
            Developer newDev = new Developer();
            Console.Write("First Name: ");
            newDev.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            newDev.LastName = Console.ReadLine();

            Console.WriteLine("Select their skill set:\n" +
                "1. Front End\n" +
                "2. Back End\n" +
                "3. Testing\n");

            newDev.SkillSet = (SkillSet)int.Parse(Console.ReadLine());

            Console.Write("Access to Plural Sight: ");
            switch (Console.ReadLine().ToLower())
            {
                case "true":
                case "yes":
                    newDev.HasPluralsight = true; //false by default
                    break;
            }
            if (_devTeamRepo.UpdateDeveloper(id, newDev))
            {
                Success();
            }
            else
                Failure("");
        }
        private void UpdateDevTeam()
        {
            Console.Clear();
            Console.WriteLine("Update Dev Team");
            DisplayAllDevTeams();

            Console.Write("Enter a Dev Team Id: ");
            var id = Int32.Parse(Console.ReadLine());

            DevTeam team = new DevTeam();
            Console.Write("Team Name: ");
            team.TeamName = Console.ReadLine();

            if (_devTeamRepo.UpdateDevTeam(id, team))
            {
                Success();
            }
            else
                Failure("");
        }
        private void AddDevToTeam()
        {
            Console.Clear();
            DisplayAllDevTeams();
            Console.WriteLine("Which team do you want to add a developer to?");
            Console.Write("Team Id: ");
            var teamId = Int32.Parse(Console.ReadLine());
            if (AddDevToThisTeam(teamId))
            {
                if (AddDevLoop(teamId))
                    Success();
                else
                    Failure("");
            }
            else
                Failure("");
        }
        private bool AddDevLoop(int teamId)
        {
            Console.WriteLine("Do you wish to add a devloper to this team?");
            Console.Write($"Team {teamId} y/n: ");
            bool goAgain = true;
            switch (Console.ReadLine().ToLower())
            {
                case "y":
                case "yes":
                case "dev":
                    break;
                default:
                    goAgain = false;
                    break;
            }
            while (goAgain)
            {
                if (AddDevToThisTeam(teamId))
                {
                    AddDevLoop(teamId);
                    return true;
                }
                else
                    return false;
            }
            return true;
        }
        private bool AddDevToThisTeam(int teamId)
        {
            Console.Clear();
            DisplayAllDevs();
            Console.WriteLine("Which developer would you like to add?");
            Console.Write("Dev Id: ");
            int devId = Int32.Parse(Console.ReadLine());
            return _devTeamRepo.AddDevloperToTeamById(devId, teamId);
        }
        // Delete
        private void DeleteDev()
        {
            Console.Clear();

            List<Developer> devs = _devTeamRepo.GetAllDevelopers();
            int count = 0;

            foreach (var dev in devs)
            {
                count++;
                Console.WriteLine($"{count:00}. {dev.FullName}");
            }
            Console.Write("What dev do you want to remove: ");
            int targetId = int.Parse(Console.ReadLine());
            int targetIndex = targetId - 1;
            if (targetIndex >= 0 && targetIndex < devs.Count())
            {
                Developer devToDelete = devs[targetIndex];

                if (_devTeamRepo.DeleteDeveloper(devToDelete))
                {
                    Console.WriteLine($"{devToDelete.FullName} deleted successfully");
                    Success();
                }
                else
                    Failure("");
            }
            else
                Failure("No Developer has that ID");
        }
        private void DeleteDevTeam()
        {
            Console.Clear();
            DisplayAllDevTeams();
            Console.WriteLine("What dev team do you want to remove.");
            Console.Write("Team Id: ");
            int targetId = int.Parse(Console.ReadLine());
            DevTeam devToDelete = _devTeamRepo.GetDevTeamById(targetId);
            if (devToDelete != null)
            {

                if (_devTeamRepo.DeleteDevTeam(devToDelete))
                {
                    Console.WriteLine($"{devToDelete.TeamName} deleted successfully");
                    Success();
                }
                else
                    Failure("");
            }
            else
                Failure("No Dev Team has that ID");

        }
        private void RemoveDevFromTeam()
        {
            Console.Clear();
            Console.Write("Enter Team Id: ");
            DevTeam team = _devTeamRepo.GetDevTeamById(Int32.Parse(Console.ReadLine()));
            if (team != null)
            {
                DisplayDevTeam(team);
                Console.WriteLine("\nDevelopers:\n");
                foreach (var dev in team.Developers)
                {
                    Console.WriteLine();
                    DisplayDeveloper(dev);
                }
                Console.WriteLine("Enter a dev id to remove.");
                Console.Write("Dev Id: ");
                var id = Int32.Parse(Console.ReadLine());
                if (_devTeamRepo.RemoveDeveloperFromTeamById(id, team.TeamdId))
                    Success();
                else
                    Failure("Id not found or error.");
                AnyKey();
            }
            else
                Failure("Team not found by that id.");
        }

        private void SeedDevs()
        {
            _devTeamRepo = new DevTeamsRepo();
            Developer dev = new Developer("Dinesh", "Chugtai", SkillSet.Testing, true);
            Developer dev1 = new Developer("Richard", "Hendricks", SkillSet.BackEnd, true);
            Developer dev2 = new Developer("Bertram", "Gilfoyle", SkillSet.BackEnd, false);
            Developer dev3 = new Developer("Jared", "Dunn", SkillSet.Testing, true);
            DevTeam devT = new DevTeam("Pied Piper");
            _devTeamRepo.CreateDeveloper(dev);
            _devTeamRepo.CreateDeveloper(dev1);
            _devTeamRepo.CreateDeveloper(dev2);
            _devTeamRepo.CreateDeveloper(dev3);
            devT.Developers.Add(dev);
            devT.Developers.Add(dev1);
            devT.Developers.Add(dev2);
            devT.Developers.Add(dev3);
            _devTeamRepo.CreateDevTeam(devT);
        }
        private void DisplayDeveloperSimple(Developer dev)
        {
            Console.WriteLine($"Id: {dev.DevloperId}\n" +
                $"Name: {dev.FullName}\n");
        }
        private void DisplayDeveloper(Developer dev)
        {
            Console.WriteLine($"Id: {dev.DevloperId}\n" +
                $"Name: {dev.FullName}\n" +
                $"Skill Set: {dev.SkillSet}\n" +
                $"Has Pluralsight: {(dev.HasPluralsight ? "Yes" : "No")}");
        }
        private void DisplayDevTeam(DevTeam devTeam)
        {
            Console.WriteLine($"Id: {devTeam.TeamdId}\n" +
                $"Name: {devTeam.TeamName}\n" +
                $"Members: {devTeam.Developers.Count()}");
        }
        private void DisplayAllDevs()
        {
            List<Developer> listOfDevs = _devTeamRepo.GetAllDevelopers();
            foreach (var dev in listOfDevs)
            {
                DisplayDeveloperSimple(dev);
                Console.WriteLine();
            }
        }
        private void DisplayAllDevTeams()
        {
            List<DevTeam> devTeams = _devTeamRepo.GetAllDevTeams();
            foreach (var devTeam in devTeams)
            {
                DisplayDevTeam(devTeam);
            }
        }
        private void Success()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Success! ");
            AnyKey();
            Console.ResetColor();
        }
        private void Failure(string error)
        {
            if (error == "") error = "Failure!";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{error} ");
            AnyKey();
            Console.ResetColor();
        }
        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
