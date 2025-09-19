using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour
{
  [SerializeField] float sceneTransitionDelay = 2f; // Zamanlayıcı süresi

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

  private void HandleTargetFound()
  {
    Debug.Log("Target was found! Loading next scene in " + sceneTransitionDelay + " seconds...");

    // Unsubscribe before loading scene to prevent issues
    TargetObject.OnTargetFound -= HandleTargetFound;

    // Start coroutine with delay
    StartCoroutine(LoadNextSceneWithDelay());
  }

  private IEnumerator LoadNextSceneWithDelay()
  {
    // Wait for specified delay
    yield return new WaitForSeconds(sceneTransitionDelay);

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