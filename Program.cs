using ConsoleTables;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using System.Text;

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
                break;
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
            Console.WriteLine($"The move was - {args[computerMove]}");
        }
    }

    public static void PrintAvailableMoves(string[] moves)
    {
        for (int i = 0; i < moves.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {moves[i]}");
        }
        Console.WriteLine("0 - exit");
        Console.WriteLine("? - help");
    }
    public static class RuleDefiner
    {
        public static string DetermineWinOrLose(int numberOfMoves, int moveOfTheComputer, int moveOfTheUser)
        {
            int halfOfNumberOfValues = numberOfMoves / 2;
            int valueOfCell = Math.Sign( (moveOfTheUser - moveOfTheComputer + halfOfNumberOfValues + numberOfMoves)
                % numberOfMoves - halfOfNumberOfValues);
            if (valueOfCell == 0)
            {
                return "Draw";
            }
            else if (valueOfCell == 1)
            {
                return "Win";
            }
            else
            {
                return "Lose";
            }
        }
    }

    public static class TableGenerator
    { 
        public static ConsoleTable GenerateTable(string[] namesOfMoves)
        {
            var columns = GenerateColumnNames(namesOfMoves);
            var table = new ConsoleTable(columns);
            FillTableWithData(table, namesOfMoves);

            return table;
        }

        public static string[] GenerateColumnNames(string[] namesOfMoves)
        {
            string[] columnNames = new string[namesOfMoves.Length + 1];
            columnNames[0] = "v PC\\User >";
            for (int i = 1; i < namesOfMoves.Length + 1; i++)
            {
                columnNames[i] = namesOfMoves[i-1];
            }

            return columnNames;
        }
        public static void FillTableWithData(ConsoleTable table, string[] namesOfMoves)
        {
            for (int i = 0; i < namesOfMoves.Length; i++)
            {
                table.AddRow(GenerateRowData(namesOfMoves, i));
            }
        }
        
        public static string[] GenerateRowData(string[] namesOfMoves, int rowNumber)
        {
            string[] rows = new string[namesOfMoves.Length + 1];
            rows[0] = namesOfMoves[rowNumber];
            for (int j = 1; j < namesOfMoves.Length + 1; j++)
            {
                rows[j] = RuleDefiner.DetermineWinOrLose(namesOfMoves.Length, rowNumber, j-1);
            }

            return rows;
        }

    }

    public static class Error
    {
        public static bool WrongMoves(string[] moves)
        {
            return moves.Length == 0 || moves.Length % 2 == 0;
        }

        public static bool CheckForAppropriateOption(string? chosenOption, string[] namesOfMoves)
        {
            int number;
            if ( !(int.TryParse(chosenOption, out number) || chosenOption == "0" || chosenOption == "?") )
            {
                return true;
            }
            return false;
        }
    }

    public static class HMacSha3
    {
        public static string CalculateHMACSHA3(byte[] data, byte[] key)
        {
            var digest = new Sha3Digest(256);
            var hmac = new HMac(digest);
            hmac.Init(new KeyParameter(key));

            hmac.BlockUpdate(data, 0, data.Length);
            byte[] result = new byte[hmac.GetMacSize()];
            hmac.DoFinal(result, 0);

            return BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();

            
        }
    }

    public static class Randomiser
    {
        public static int RandomMove(string[] namesOfMoves)
        {
            Random _rnd = new Random();
            int move = _rnd.Next(0, namesOfMoves.Length);
            return move;
        }

        public static byte[] RandomKey()
        {
            using (RandomNumberGenerator  rnd = RandomNumberGenerator.Create()) 
            {
                var key = new byte[256];
                rnd.GetBytes(key);
                return key;
            }

        }
    }

}