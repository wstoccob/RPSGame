namespace RPSGame.Tools;

public static class RuleDefiner
{
    public static string DetermineWinOrLose(int numberOfMoves, int moveOfTheComputer, int moveOfTheUser)
    {
        int halfOfNumberOfValues = numberOfMoves / 2;
        int valueOfCell = Math.Sign( (moveOfTheUser - moveOfTheComputer + halfOfNumberOfValues + numberOfMoves)
            % numberOfMoves - halfOfNumberOfValues);
        return valueOfCell switch
        {
            0 => "Draw",
            1 => "Win",
            -1 => "Lose",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}