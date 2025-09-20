using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
  [SerializeField] float sceneTransitionDelay = 5f; // Zamanlayıcı süresi
  [SerializeField] float roomADelay = 60;
  [SerializeField] float roomBDelay = 40;

  [Header("UI Text Components")]
  [SerializeField] TextMeshProUGUI levelText;      // Level bilgisi
  [SerializeField] TextMeshProUGUI timerText;      // Zamanlayıcı
  [SerializeField] TextMeshProUGUI subtitleText;   // Altyazı

  // Coroutine reference to cancel if target is found
  private Coroutine roomBTimeoutCoroutine;
  private Coroutine roomATimeoutCoroutine;
  private Coroutine timerCountdownCoroutine;

  void Start()
  {
    // Subscribe to static event (any target found)
    TargetObject.OnTargetFound += HandleTargetFound;

    // Initialize UI
    InitializeUI();
  }

  private void InitializeUI()
  {
    string sceneName = SceneManager.GetActiveScene().name;
    // Set level text
    if (levelText != null)
    {
      levelText.text = GameInfoTexts.GetLevelText(sceneName);
    }

    // Clear subtitle initially
    UpdateUIText(subtitleText,
    sceneName.Contains("A") ? GameInfoTexts.wasdMovement : GameInfoTexts.clickTarget);

    // Hide timer initially
    if (timerText != null)
    {
      timerText.gameObject.SetActive(false);
    }
  }

  void OnDestroy()
  {
    // Always unsubscribe to prevent memory leaks
    TargetObject.OnTargetFound -= HandleTargetFound;
  }

  public void HandleRoomA()
  {
    Debug.Log("You have limited time for checking the room, be quick");

    // Update UI
    UpdateUIText(subtitleText, GameInfoTexts.RoomASubtitle);

    if (timerText != null)
    {
      timerText.gameObject.SetActive(true);
    }

    // Start coroutine with delay and timer
    roomATimeoutCoroutine = StartCoroutine(LoadRoomBOnTimeout(roomADelay));
  }

  public void HandleRoomB()
  {
    Debug.Log("Find the similar object, quick!");

    // Update UI
    UpdateUIText(subtitleText, GameInfoTexts.RoomBSubtitle);

    if (timerText != null)
    {
      timerText.gameObject.SetActive(true);
    }

    // Start coroutine with delay and timer
    // If time is up, go back to room A of the same level
    roomBTimeoutCoroutine = StartCoroutine(LoadRoomAOnTimeout(roomBDelay));
  }

  // Go to the next level when Target is found in room B
  private void HandleTargetFound()
  {
    Debug.Log(GameInfoTexts.TargetFound);

    // Cancel the Room B timeout coroutine if it's running
    if (roomBTimeoutCoroutine != null)
    {
      StopCoroutine(roomBTimeoutCoroutine);
      roomBTimeoutCoroutine = null;
      Debug.Log("Room B timeout coroutine cancelled - target found!");
    }

    // Cancel the timer countdown coroutine if it's running
    if (timerCountdownCoroutine != null)
    {
      StopCoroutine(timerCountdownCoroutine);
      timerCountdownCoroutine = null;
      Debug.Log("Timer countdown coroutine cancelled - target found!");
    }

    // Unsubscribe before loading scene to prevent issues
    TargetObject.OnTargetFound -= HandleTargetFound;

    // Update subtitle
    UpdateUIText(subtitleText, GameInfoTexts.TargetFound);

    // Start coroutine with delay
    StartCoroutine(LoadNextSceneWithDelayAndTimer(sceneTransitionDelay));
  }

  private IEnumerator LoadNextSceneWithDelayAndTimer(float timeDelay)
  {
    // Use the shared timer update method
    timerCountdownCoroutine = StartCoroutine(UpdateTimerCountdown(timeDelay));
    yield return timerCountdownCoroutine;

    // Time's up - load next scene
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
      Debug.LogWarning("No next scene available in build settings!");
    }
  }

  // If you can not find the object in room B reload room A
  private IEnumerator LoadRoomAOnTimeout(float timeDelay)
  {

    // Cancel the Room B timeout coroutine if it's running
    if (roomBTimeoutCoroutine != null)
    {
      StopCoroutine(roomBTimeoutCoroutine);
      roomBTimeoutCoroutine = null;
      Debug.Log("Room B timeout coroutine cancelled - target found!");
    }

    // Cancel the timer countdown coroutine if it's running
    if (timerCountdownCoroutine != null)
    {
      StopCoroutine(timerCountdownCoroutine);
      timerCountdownCoroutine = null;
      Debug.Log("Timer countdown coroutine cancelled - target found!");
    }

    // Use the shared timer update method
    timerCountdownCoroutine = StartCoroutine(UpdateTimerCountdown(timeDelay));
    yield return timerCountdownCoroutine;

    // Time's up in Room B - go back to Room A of the same level
    string currentSceneName = SceneManager.GetActiveScene().name;

    // Replace B with A to get Room A scene name
    string roomASceneName = currentSceneName.Substring(0, currentSceneName.Length - 1) + "A";

    // Update UI to show failure message
    UpdateUIText(subtitleText, GameInfoTexts.TimeUpGoingBack);

    // Clear the coroutine references since it's completing normally
    roomBTimeoutCoroutine = null;
    timerCountdownCoroutine = null;

    // Wait a moment before loading
    yield return new WaitForSeconds(sceneTransitionDelay);

    SceneManager.LoadScene(roomASceneName);
  }

  // After examining the room A go to the room B
  private IEnumerator LoadRoomBOnTimeout(float timeDelay)
  {
    // Cancel the Room A timeout coroutine if it's running
    if (roomATimeoutCoroutine != null)
    {
      StopCoroutine(roomATimeoutCoroutine);
      roomATimeoutCoroutine = null;
      Debug.Log("Room A timeout coroutine cancelled - target found!");
    }

    // Cancel the timer countdown coroutine if it's running
    if (timerCountdownCoroutine != null)
    {
      StopCoroutine(timerCountdownCoroutine);
      timerCountdownCoroutine = null;
      Debug.Log("Timer countdown coroutine cancelled - target found!");
    }

    // Use the shared timer update method
    timerCountdownCoroutine = StartCoroutine(UpdateTimerCountdown(timeDelay));
    yield return timerCountdownCoroutine;

    // Time's up in Room A - go to Room B of the same level
    string currentSceneName = SceneManager.GetActiveScene().name;

    // Replace A with B to get Room B scene name
    string roomBSceneName = currentSceneName.Substring(0, currentSceneName.Length - 1) + "B";

    // Update UI to show transition message (you can customize this)
    UpdateUIText(subtitleText, GameInfoTexts.TimeUpGoingToRoomB);

    // Clear the coroutine references since it's completing normally
    roomATimeoutCoroutine = null;
    timerCountdownCoroutine = null;

    // Wait a moment before loading
    yield return new WaitForSeconds(sceneTransitionDelay);

    SceneManager.LoadScene(roomBSceneName);
  }

  // Shared method for timer countdown update
  private IEnumerator UpdateTimerCountdown(float timeDelay)
  {
    float remainingTime = timeDelay;

    // Update timer every second
    while (remainingTime > 0)
    {
      // Update timer text
      if (timerText != null)
      {
        timerText.text = GameInfoTexts.GetTimerText(remainingTime);
      }

      yield return new WaitForSeconds(1f);
      remainingTime -= 1f;
    }

    // Clear the timer reference when countdown completes normally
    timerCountdownCoroutine = null;
  }

  private void UpdateUIText(TextMeshProUGUI TMP, String text)
  {
    if (TMP != null)
    {
      TMP.text = text;
    }
  }
}