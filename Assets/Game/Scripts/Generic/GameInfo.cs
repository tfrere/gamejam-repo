using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameInfo
{
    public static int numberOfPlayers = 2; // set to minimum of two
    public static int[] playerScores = { 0, 0, 0, 0 };
    public static int[] playerArrows = { initialArrows, initialArrows, initialArrows, initialArrows };
    public static string sceneToLoad;
    public static string gameState = "menu";
    public static string inputSchemeState = "game";
    public static int initialArrows = 30;
    public static int maxScore = 10;

    public static void resetScores() {
        playerScores[0] = 0;
        playerScores[1] = 0;
        playerScores[2] = 0;
        playerScores[3] = 0;
    }
}