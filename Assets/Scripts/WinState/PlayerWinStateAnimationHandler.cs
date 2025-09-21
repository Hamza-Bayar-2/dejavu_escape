using TMPro;
using UnityEngine;

public class PlayerWinStateAnimationHandler : MonoBehaviour
{
  Animator playerAnimator;
  [SerializeField] TextMeshProUGUI subtitleText;   // Altyazı

  void Awake()
  {
    playerAnimator = GetComponent<Animator>();
    if (GamePageManager.Instance != null)
    {
      GamePageManager.Instance.backgroundAudio.Stop();
    }
  }

  public void DisableAnimator()
  {
    playerAnimator.enabled = false;
  }

  public void EmptyText()
  {
    if (subtitleText != null)
    {
      subtitleText.text = "";
    }
  }

  public void Shock()
  {
    if (subtitleText != null)
    {
      subtitleText.text = "What...";
    }
  }

  public void ItIsJustDream()
  {
    if (subtitleText != null)
    {
      subtitleText.text = "Oh, it is just a dream";
    }
  }
}