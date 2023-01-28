using System.IO;
using RobotSim;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0) ProcessCommandsFromFile(args[0]);
        else ProcessCommandsAsInput();
    }

    static void ProcessCommandsAsInput()
    {
        Robot robot = new Robot();
        Console.WriteLine("Your robot is ready to receive commands.");
        string command;
        
        do
        {
            Console.Write("> ");
            command = Console.ReadLine();
            ProcessCommand(robot, command);    
        } while (!String.Equals(command, "EXIT", StringComparison.OrdinalIgnoreCase));
    }

    static void ProcessCommandsFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("Provided filename does not exist.");
            return;
        }

        Robot robot = new Robot();
        foreach(string command in File.ReadLines(filename))
        {
            ProcessCommand(robot, command);
        }

        Console.WriteLine("File has been processed, press any key to exit.");
        Console.ReadKey();
    }

    static void ProcessCommand(Robot robot, string input)
    {
        if (input is null) return;

        string command = input.Trim().ToUpper();
        switch (command)
        {
            case "MOVE":
            {
                robot.Move();
                break;
            }
            case "LEFT":
            {
                robot.Left();
                break;
            }
            case "RIGHT":
            {
                robot.Right();
                break;
            }
            case "REPORT":
            {
                robot.Report();
                break;
            }
            case "HELP":
            {
                Console.WriteLine("Your robot can process the following commands: PLACE, MOVE, LEFT, RIGHT, REPORT, HELP, EXIT");
                break;
            }
            default:
            {
                if (!command.StartsWith("PLACE ")) break; // Invalid command or EXIT, just ignore

                string[] commandArgs = command.Length > 6 ? command.Substring(6).Split(',') : new string[0];
                bool success = commandArgs.Length == 3;

                int x = 0;
                int y = 0;
                Facing facing = 0;
                if (success) success = int.TryParse(commandArgs[0].Trim(), out x);
                if (success) success = int.TryParse(commandArgs[1].Trim(), out y);
                if (success) success = Enum.TryParse<Facing>(commandArgs[2].Trim(), true, out facing);

                if (success) robot.Place(x, y, facing);
                else Console.WriteLine("Invalid use of PLACE command, use format PLACE x,y,facing");
                break;
            }
        }
    }
}