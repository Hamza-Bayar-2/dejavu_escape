using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  [SerializeField] float sceneTransitionDelay = 2f; // Zamanlayıcı süresi
  [SerializeField] float roomADelay = 30;
  [SerializeField] float roomBDelay = 60;

  void Start()
  {
    // Subscribe to static event (any target found)
    TargetObject.OnTargetFound += HandleTargetFound;
  }

  void OnDestroy()
  {
    // Always unsubscribe to prevent memory leaks
    TargetObject.OnTargetFound -= HandleTargetFound;
  }

  public void HandleRoomA()
  {
    Debug.Log("You have limited time for checking the room, be quick");

    // Start coroutine with delay
    StartCoroutine(LoadNextSceneWithDelay(roomADelay));
  }

  public void HandleRoomB()
  {
    Debug.Log("Find the similar object, quick!");

    // Start coroutine with delay
    StartCoroutine(LoadNextSceneWithDelay(roomBDelay));
  }

  // Go to the next level when Target is found in room B
  private void HandleTargetFound()
  {
    Debug.Log("Target was found! Loading next scene in " + sceneTransitionDelay + " seconds...");

    // Unsubscribe before loading scene to prevent issues
    TargetObject.OnTargetFound -= HandleTargetFound;

    // Start coroutine with delay
    StartCoroutine(LoadNextSceneWithDelay(sceneTransitionDelay));
  }

  private IEnumerator LoadNextSceneWithDelay(float timeDelay)
  {
    // Wait for specified delay
    yield return new WaitForSeconds(timeDelay);

    // Load next scene by build index
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    // Check if next scene exists
    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
      Debug.LogWarning("No next scene available in build settings!");
      // Optional: Load first scene or main menu
      // SceneManager.LoadScene(0);
    }
  }
}