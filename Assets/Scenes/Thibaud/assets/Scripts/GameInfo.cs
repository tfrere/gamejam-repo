public static class GameInfo
{
    private static int playerOneScore = 0;
    private static int playerTwoScore = 0;
    private static int level = 0;

    public static int PlayerOneScore 
    {
        get 
        {
            return playerOneScore;
        }
        set 
        {
            playerOneScore = value;
        }
    }

    public static int PlayerTwoScore 
    {
        get 
        {
            return playerTwoScore;
        }
        set 
        {
            playerTwoScore = value;
        }
    }

    public static int Level 
    {
        get 
        {
            return level;
        }
        set 
        {
            level = value;
        }
    }

}