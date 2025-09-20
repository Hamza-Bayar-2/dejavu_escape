using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPageManager : MonoBehaviour
{
  public void StartGame()
  {
    SceneManager.LoadScene("Level1A");
  }

  public void ExitGame()
  {
    Application.Quit();
  }
}