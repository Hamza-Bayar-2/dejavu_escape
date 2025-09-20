using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinRoomEnterd : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI subtitleText;   // Altyazı
  private bool hasTriggered = false; // Prevent multiple triggers

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && subtitleText != null && !hasTriggered)
    {
      hasTriggered = true;
      subtitleText.text = "What?... What happining...";

      // Start the 5-second timer coroutine
      StartCoroutine(LoadStartPageAfterDelay());
    }
  }

  private IEnumerator LoadStartPageAfterDelay()
  {
    // Wait for 5 seconds
    yield return new WaitForSeconds(5f);

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    // Load the StartPage scene
    SceneManager.LoadScene("StartPage");
  }
}