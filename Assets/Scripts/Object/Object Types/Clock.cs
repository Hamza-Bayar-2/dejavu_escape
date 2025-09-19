using UnityEngine;
using UnityEngine.InputSystem;

public class Clock : ObjectInteraction
{
  private Animator ClockAnimator;
  private bool isOpen;

  void Start()
  {
    ClockAnimator = GetComponent<Animator>();
    if (ClockAnimator == null)
    {
      ClockAnimator = GetComponentInChildren<Animator>();
    }
  }

  void Update()
  {
    HandleClockState();
  }

  public void OnInteract(InputValue value)
  {
    if (InsideRange && value.isPressed)
    {
      ToggleClock();
    }
  }

  private void ToggleClock()
  {
    isOpen = !isOpen;
    Debug.Log("Clock " + (isOpen ? "true" : "false"));
  }

  private void HandleClockState()
  {
    if (ClockAnimator == null || !InsideRange)
    {
      return;
    }

    // It is enouph to check just one of the Clock if it is open or not
    if (isOpen)
    {
      if (!ClockAnimator.GetBool("Should Spin"))
      {
        ClockAnimator.SetBool("Should Spin", true);
      }

    }
    else if (!isOpen)
    {
      if (ClockAnimator.GetBool("Should Spin"))
      {
        ClockAnimator.SetBool("Should Spin", false);
      }
    }
  }
}