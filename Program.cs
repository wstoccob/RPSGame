using System.Text;
using RPSGame.Tools;

internal class Program
{
    private static void Main(string[] args)
    {
        bool isRunning = true;
        while (isRunning) 
        {
            if (Error.WrongMoves(args))
            {
                Console.WriteLine("You entered invalid number of moves. Try to give me odd number of moves more than one! \n" +
                    "For example: rock paper scissors");
                break;
            }
            if (Error.CheckForRepeatedValue(args))
            {
                Console.WriteLine("It seems like you entered some similar values as names of moves. Try to give me distinct names of moves!");
                break;
            }

            int computerMove = Randomiser.RandomMove(args);
            byte[] keyForHMAC = Randomiser.RandomKey();
            string hmacHash = HMacSha3.CalculateHMACSHA3(Encoding.ASCII.GetBytes(args[computerMove]), keyForHMAC);

            Console.WriteLine(hmacHash);
            Console.WriteLine("Available moves:");
            PrintAvailableMoves(args);
            string? moveLine = Console.ReadLine();

            if (Error.CheckForAppropriateOption(moveLine, args))
            {
                Console.WriteLine("You chose inappropriate option. Try to choose something from the list!");
                continue;
            }

            if (moveLine == "?")
            {
                TableGenerator.GenerateTable(args).Write();
                continue;
            }

            int moveNumber;
            moveNumber = int.Parse(moveLine) - 1;

            if(moveNumber == -1)
            {
                break;
            }

            Console.WriteLine("It is " + RuleDefiner.DetermineWinOrLose(args.Length, computerMove, moveNumber));
            Console.WriteLine(Convert.ToBase64String(keyForHMAC));
            Console.WriteLine($"The move was - {args[computerMove]} \n \n");
        }
    }

    private static void PrintAvailableMoves(string[] moves)
    {
        for (var i = 0; i < moves.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {moves[i]}");
        }

        Console.WriteLine("0 - exit");
        Console.WriteLine("? - help");
    }
}