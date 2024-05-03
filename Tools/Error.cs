namespace RPSGame.Tools;

public static class Error
{
    public static bool WrongMoves(string[] moves)
    {
        return moves.Length == 0 || moves.Length == 1 || moves.Length % 2 == 0;
    }

    public static bool CheckForAppropriateOption(string? chosenOption, string[] namesOfMoves)
    {
        if ( int.TryParse(chosenOption, out var number) )
        {
            return number > namesOfMoves.Length || number < 0;
        }
        return chosenOption != "0" && chosenOption != "?";
    }
    
    public static bool CheckForRepeatedValue(string[] namesOfMoves)
    {
        return namesOfMoves.Length != namesOfMoves.Distinct().Count();
    }
}