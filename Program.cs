using ConsoleTables;

internal class Program
{
    private static void Main(string[] args)
    {
        TableGenerator.GenerateTable(args).Write();
    }

    public class RuleDefiner
    {
        public void DefineRulesForTable(string[] args)
        {

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
                table.AddRow(namesOfMoves[i], "column2", "column3","column4");
            }
        }

    }



}