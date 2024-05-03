namespace RPSGame.Tools;

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