using ConsoleTables;

namespace RPSGame.Tools;

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