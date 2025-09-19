using UnityEngine;
using UnityEngine.InputSystem;

public class Door : ObjectInteraction
{
  private Animator DoorAnimator;
  private bool isOpen;

  void Start()
  {
    DoorAnimator = GetComponent<Animator>();
    if (DoorAnimator == null)
    {
      DoorAnimator = GetComponentInChildren<Animator>();
    }
  }

  void Update()
  {
    HandleDoorState();
  }

  public void OnInteract(InputValue value)
  {
    if (InsideRange && value.isPressed)
    {
      ToggleDoor();
    }
  }

  private void ToggleDoor()
  {
    isOpen = !isOpen;
    Debug.Log("Door " + (isOpen ? "opened" : "closed"));
  }

  private void HandleDoorState()
  {
    if (DoorAnimator == null || !InsideRange)
    {
      return;
    }

    // It is enouph to check just one of the Door if it is open or not
    if (isOpen)
    {
      if (!DoorAnimator.GetBool("Is Door Open"))
      {
        DoorAnimator.SetBool("Is Door Open", true);
      }

    }
    else if (!isOpen)
    {
      if (DoorAnimator.GetBool("Is Door Open"))
      {
        DoorAnimator.SetBool("Is Door Open", false);
      }
    }
  }
}