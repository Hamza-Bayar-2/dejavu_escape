using UnityEngine;

public static class GameInfoTexts
{
  public static string RoomA = "Observation";
  public static string RoomB = "Search";
  public static string wasdMovement = "WASD for moving";
  public static string clickTarget = "Enter the room and left click if you find the Object";

  public static string Level = "Level: 1";
  public static string Time = "Time: 00:00";
  public static string GameOver = "Game Over";
  public static string Victory = "Victory!";
  public static string Loading = "Loading...";
  public static string Instructions = "Use WASD to move";

  // Room A (Gözlem Odası) Texts
  public static string RoomASubtitle = "You have limited time for observing the room";

  // Room B (Etkileşim Odası) Texts  
  public static string RoomBSubtitle = "Find the similar object, quick!";
  public static string PressEInteract = "Press E to interact";

  // Target Found
  public static string TargetFound = "Target found! Loading next scene...";

  // Time's up message
  public static string TimeUpGoingBack = "Time's up! Going back to observation room...";
  public static string TimeUpGoingToRoomB = "Investigation complete. Proceeding to search phase...";

  // Timer Format
  public static string GetTimerText(float timeInSeconds)
  {
    int minutes = Mathf.FloorToInt(timeInSeconds / 60);
    int seconds = Mathf.FloorToInt(timeInSeconds % 60);
    return string.Format("Time: {0:00}:{1:00}", minutes, seconds);
  }

  // Level Format
  public static string GetLevelText(string sceneName)
  {
    // Extract the level number from scene name (e.g., "Level1A" -> "1")
    string levelNumber = "";
    for (int i = 0; i < sceneName.Length; i++)
    {
      if (char.IsDigit(sceneName[i]))
      {
        levelNumber += sceneName[i];
      }
      else if (levelNumber.Length > 0) // Stop when we hit a non-digit after finding digits
      {
        break;
      }
    }
    
    bool isRoomA = sceneName.Contains("A");
    string roomType = isRoomA ? RoomA : RoomB;
    return "Level " + levelNumber + ": " + roomType;
  }
}