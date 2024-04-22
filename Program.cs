using ConsoleTables;

internal class Program
{
    private static void Main(string[] args)
    {
        TableGenerator.GenerateTable(args).Write();
    }

    public static class RuleDefiner
    {
        public static string DetermineWinOrLoss(int numberOfMoves, int moveOfTheComputer, int moveOfTheUser)
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
                return "Loss";
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
                rows[j] = RuleDefiner.DetermineWinOrLoss(namesOfMoves.Length, rowNumber, j-1);
            }

            return rows;
        }

    }



}