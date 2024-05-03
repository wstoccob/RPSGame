namespace RPSGame.Tools;

public static class Error
{
    public static bool WrongMoves(string[] moves)
    {
        return moves.Length == 0 || moves.Length == 1 || moves.Length % 2 == 0;
    }

    public static bool CheckForAppropriateOption(string? chosenOption, string[] namesOfMoves)
    {
        int number;
        if ( int.TryParse(chosenOption, out number) )
        {
            if( number > namesOfMoves.Length || number < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (chosenOption == "0" || chosenOption == "?")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    
    public static bool CheckForRepeatedValue(string[] namesOfMoves)
    {
        if ( namesOfMoves.Length != namesOfMoves.Distinct().Count())
        {
            return true;
        }
        return false;
    }
}