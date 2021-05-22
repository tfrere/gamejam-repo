using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameInfo
{
    private static int playerOneScore = 0;
    private static int playerTwoScore = 0;

    private static List<int> playerScores;

    private static int initialArrows = 30;
    private static int playerOneArrows = initialArrows;
    private static int playerTwoArrows = initialArrows;

    private static List<int> playerAvailableArrows;


    private static PlayerInput playerOneInput;
    private static PlayerInput playerTwoInput;

    private static List<PlayerInput> playerInputs;

    private static int level = 0;
    private static int maxScore = 10;
    public static string sceneToLoad;

    public static string gameState = "menu";


    public static string GameState 
    {
        get 
        {
            return gameState;
        }
        set 
        {
            gameState = value;
        }
    }




    public static int InitialArrows 
    {
        get 
        {
            return initialArrows;
        }
    }


    public static PlayerInput PlayerOneInput 
    {
        get 
        {
            return playerOneInput;
        }
        set 
        {
            playerOneInput = value;
        }
    }

    public static PlayerInput PlayerTwoInput 
    {
        get 
        {
            return playerTwoInput;
        }
        set 
        {
            playerTwoInput = value;
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