using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinRoomEnterd : MonoBehaviour
{
  
  [SerializeField] TextMeshProUGUI subtitleText;   // Altyazı
  [SerializeField] private AudioSource roomEnteredAudio; // Audio source for room entered sound
  private bool hasTriggered = false; // Prevent multiple triggers

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && subtitleText != null && !hasTriggered)
    {
      hasTriggered = true;
      subtitleText.text = "What?... What happining...";

      // Play room entered sound
      if (roomEnteredAudio != null)
      {
        roomEnteredAudio.Play();
      }

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