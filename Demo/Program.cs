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

                success &= int.TryParse(commandArgs[0].Trim(), out int x);
                success &= int.TryParse(commandArgs[1].Trim(), out int y);
                success &= Enum.TryParse<Facing>(commandArgs[2].Trim(), out Facing facing);

                if (success) robot.Place(x, y, facing);
                else Console.WriteLine("Invalid use of PLACE command, use format PLACE x,y,facing");
                break;
            }
        }
    }
}