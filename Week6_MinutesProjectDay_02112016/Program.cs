using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Week6_MinutesProjectDay_02112016
{
    class Program
    {
        //The Main() method Resets the screen to clear and add a header.  The Menu() method is run.
        static void Main(string[] args)
        {
            Reset();
            Menu();
        }

        //Reset() can be called to clear the screen and add the header to the top of the console.
        static void Reset()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("  MEETING MINUTES MANAGEMENT SOFTWARE");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        } 

        //Menu() is the Main Menu of the program.  It is called by the Main() method
        //The Menu() method begins by resetting the screen.
        //Menu() has 3 user options- Create Minutes, View Team, and Exit.  Each option calls a method.  If an invalid entry is pressed,
        //the user will be asked to enter a valid option.
        //It is contained in a loop that will continue until the Exit option is pressed.
        static void Menu()
        {
            bool runProgram = true;
            do
            {
                Reset();
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("\t1. Create Meeting Minutes");
                Console.WriteLine("\t2. View Team");
                Console.WriteLine("\t3. Exit");
                Console.Write("\nWhat would you like to do? ");
                string mainMenuNum = Console.ReadLine();
                switch (mainMenuNum)
                {
                    case "1": CreateMinutes(); break;
                    case "2": ViewTeam(); break;
                    case "3":
                        Exit();
                        runProgram = false;
                        break;
                    default:
                        Reset();
                        Console.WriteLine("INVALID ENTRY....\nPress any key to continue.");
                        Console.ReadKey();
                        Menu();
                        break;
                }
            } while (runProgram);
        }

        //CreateMinutes() is a method called from the main menu for the purpose of creating the minutes file.
        //The user is asked to enter heading information, such as meeting date, meeting type, recorder, and leader
        //The method calls the TeamNames() method for a list and selection of the team names to define the meeting type
        //This information, along with the company name and address, is written to the file
        //The user is asked for a meeting topic and the notes
        //The user may enter another meeting topic or end
        //Upon end the minutes are written to the screen.  When the user is finished reading the screen they may press a key to
        //continue and will return to the Main Menu.
        static void CreateMinutes()
        {
            Reset();
            Console.WriteLine("CREATE MEETING MINUTES");
            Console.WriteLine("Please enter the following information...\n");
            Console.Write("Meeting Date (MMDDYY): ");
            string date = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Meeting Type (enter a number from the list below): ");
            string team = TeamNames();
            Console.WriteLine();

            Console.Write("Meeting Minutes Recorder: ");
            string recorder = Console.ReadLine();
            Console.Write("Meeting Leader: ");
            string leader = Console.ReadLine();

            StreamWriter minutes = new StreamWriter("Minutes" + date + ".txt");
            minutes.WriteLine("---------------------------------------");
            minutes.WriteLine("\tPROGRAMMERS IN JAMMERS");
            minutes.WriteLine("\t123 Dream Lane");
            minutes.WriteLine("\tBedhead, OH 45678");
            minutes.WriteLine("---------------------------------------");
            minutes.WriteLine();
            minutes.WriteLine("\"Meeting Minutes\" for " + team +" Team");
            minutes.WriteLine("Minutes Recorded by: " + recorder);
            minutes.WriteLine("Meeting Facilitator: " + leader);

            bool anotherTopic = true;
            do
            {
                Console.Write("What is the meeting topic? ");
                string topic = Console.ReadLine();
                Console.WriteLine("Enter notes for " + topic + " : (Press enter when finished)");
                string notes = Console.ReadLine();
                minutes.WriteLine();
                minutes.WriteLine("Meeting Topic: " + topic);
                minutes.WriteLine(notes);
                Console.Write("\nDo you want to enter notes for another topic? (y or n) ");
                string answer = Console.ReadLine();
                if (answer == "n" || answer == "N")
                {
                    minutes.Close();
                    Console.WriteLine("I will print the minutes to the screen.  Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    anotherTopic = false;
                    StreamReader readMinutes = new StreamReader("Minutes" + date + ".txt");
                    int lineNum = 0;
                    string line = readMinutes.ReadLine();
                    while (line != null)
                    {
                        lineNum++;
                        Console.WriteLine(line);
                        line = readMinutes.ReadLine();
                    }
                    readMinutes.Close();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            } while (anotherTopic);
        }

        //TeamNames() defines a list of team names.  It writes them to the console and asks the user to choose one for the Meeting Type
        //It is called within the CreateMinutes() method.  It returns a string variable with the team name selection
        static string TeamNames()
        {
            List<string> teams = new List<string> { "Administration", "Programmers", "Marketing" };
            int index = 1;
            foreach (string name in teams)
            {
                Console.WriteLine(index + ". "+ name);
                index++;
            }

            string teamNum = Console.ReadLine();
            string teamName = "";
            switch(teamNum)
            {
                case "1":   teamName = teams[0]; break;
                case "2":   teamName = teams[1]; break;
                case "3":   teamName = teams[2]; break;
                default:
                    Reset();
                    Console.WriteLine("INVALID ENTRY....\n");
                    TeamNames();
                    break;
            }
            return teamName;
        }

        //ViewTeam() method is called from the Main Menu
        //It defines a dictionary with team members names as the key and team name as the value
        //The user can 
        static void ViewTeam()
        {
            Dictionary<string, string> teamDictionary = new Dictionary<string, string>()
            {
                {"Abby", "Marketing" }, {"Bob", "Programmers" }, {"Carla", "Administration" },
                {"David", "Programmers" }, {"Erin", "Marketing" }, {"Frank","Programmers" },
                {"Gavin", "Programmers" }, {"Holly", "Administration" }, {"Isabella", "Marketing" },
                {"Joe", "Administration"}
            };

            Reset();
            Console.WriteLine("VIEW TEAM");
            Console.WriteLine("\t1. Administration");
            Console.WriteLine("\t2. Programmers");
            Console.WriteLine("\t3. Marketing");
            Console.WriteLine("\t4. ALL Teams");
            Console.Write("\nWhich team would you like to view? ");
            string teamNum = Console.ReadLine();
            Console.WriteLine();
            string teamName = "";
            switch (teamNum)
            {
                case "1": teamName = "Administration"; break;
                case "2": teamName = "Programmers";  break;
                case "3": teamName = "Marketing";  break;
                case "4": teamName = "All";  break;
                default:
                    Reset();
                    Console.WriteLine("INVALID ENTRY....\nPress any key to continue.");
                    Console.ReadKey();
                    ViewTeam();
                    break;
            }

            foreach (KeyValuePair<string, string> pair in teamDictionary)
            {
                if (teamName == "All")
                {
                    Console.WriteLine("\t"+pair.Key+ " (" + pair.Value+")" );
                }
                else if (pair.Value == teamName)
                {
                    Console.WriteLine("\t"+pair.Key);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        //The Exit() method is called from the Main Menu and will exit the program
        static void Exit()
        {
            Reset();
            Console.WriteLine("Goodbye!!\n");
        }

    }
}
