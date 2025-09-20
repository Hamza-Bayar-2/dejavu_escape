using UnityEngine;
using UnityEngine.SceneManagement;

public class IsRoomEntered : MonoBehaviour
{
  private LevelManager levelManager;

  void Start()
  {
    // LevelManager'ı bul
    levelManager = FindFirstObjectByType<LevelManager>();

    if (levelManager == null)
    {
      Debug.LogError("LevelManager not found in scene!");
    }
  }

  void OnTriggerEnter(Collider other)
  {
    // Eğer çarpan obje Player tag'ına sahipse
    if (other.CompareTag("Player") && levelManager != null)
    {
      Debug.Log("Player entered the room!");
      if (SceneManager.GetActiveScene().name.Contains("Level1B"))
      {
        // Target is hidden so we set its visibility to false
        TargetObject.SetTargetVisibility(false);
      }

      // Scene ismine göre doğru metodu çağır
      string currentSceneName = SceneManager.GetActiveScene().name;

      if (currentSceneName.EndsWith("A"))
      {
        levelManager.HandleRoomA();
      }
      else if (currentSceneName.EndsWith("B"))
      {
        levelManager.HandleRoomB();
      }
      else
      {
        Debug.LogWarning("Scene name doesn't end with A or B: " + currentSceneName);
      }

      // Script'i deaktif et ki bir daha çalışmasın
      this.gameObject.SetActive(false);
    }
  }
}