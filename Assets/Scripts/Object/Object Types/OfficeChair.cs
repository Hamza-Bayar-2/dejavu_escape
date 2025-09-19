using UnityEngine;
using UnityEngine.InputSystem;

public class OfficeCair : ObjectInteraction
{
  private Animator OfficeCairAnimator;
  private bool isOpen;

  void Start()
  {
    OfficeCairAnimator = GetComponent<Animator>();
    if (OfficeCairAnimator == null)
    {
      OfficeCairAnimator = GetComponentInChildren<Animator>();
    }
  }

  void Update()
  {
    HandleOfficeCairState();
  }

  public void OnInteract(InputValue value)
  {
    if (InsideRange && value.isPressed)
    {
      ToggleOfficeCair();
    }
  }

  private void ToggleOfficeCair()
  {
    isOpen = !isOpen;
    Debug.Log("OfficeCair " + (isOpen ? "opened" : "closed"));
  }

  private void HandleOfficeCairState()
  {
    if (OfficeCairAnimator == null || !InsideRange)
    {
      return;
    }

    // It is enouph to check just one of the OfficeCair if it is open or not
    if (isOpen)
    {
      if (!OfficeCairAnimator.GetBool("Should Spin"))
      {
        OfficeCairAnimator.SetBool("Should Spin", true);
      }

    }
    else if (!isOpen)
    {
      if (OfficeCairAnimator.GetBool("Should Spin"))
      {
        OfficeCairAnimator.SetBool("Should Spin", false);
      }
    }
  }
}