public static class GameInfo
{
    private static int playerOneScore = 0;
    private static int playerTwoScore = 0;

    private static int initialArrows = 3;
    private static int playerOneArrows = initialArrows;
    private static int playerTwoArrows = initialArrows;
    private static int level = 0;
    private static int maxScore = 3;
    public static string sceneToLoad;


    public static int InitialArrows 
    {
        get 
        {
            return initialArrows;
        }
    }


    public static int PlayerOneArrows 
    {
        get 
        {
            return playerOneArrows;
        }
        set 
        {
            playerOneArrows = value;
        }
    }

    public static int PlayerTwoArrows 
    {
        get 
        {
            return playerTwoArrows;
        }
        set 
        {
            playerTwoArrows = value;
        }
    }

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

    public static int MaxScore 
    {
        get 
        {
            return maxScore;
        }
        set 
        {
            maxScore = value;
        }
    }

}