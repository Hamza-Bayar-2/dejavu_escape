using UnityEngine;
using UnityEngine.InputSystem;

public class Vase : ObjectInteraction
{
  private Animator VaseAnimator;
  private bool isOpen;

  void Start()
  {
    VaseAnimator = GetComponent<Animator>();
    if (VaseAnimator == null)
    {
      VaseAnimator = GetComponentInChildren<Animator>();
    }
  }

  void Update()
  {
    HandleVaseState();
  }

  public void OnInteract(InputValue value)
  {
    if (InsideRange && value.isPressed)
    {
      ToggleVase();
    }
  }

  private void ToggleVase()
  {
    isOpen = !isOpen;
    Debug.Log("Vase " + (isOpen ? "true" : "false"));
  }

  private void HandleVaseState()
  {
    if (VaseAnimator == null || !InsideRange)
    {
      return;
    }

    // It is enouph to check just one of the Vase if it is open or not
    if (isOpen)
    {
      if (!VaseAnimator.GetBool("Should Shake"))
      {
        VaseAnimator.SetBool("Should Shake", true);
      }

    }
    else if (!isOpen)
    {
      if (VaseAnimator.GetBool("Should Shake"))
      {
        VaseAnimator.SetBool("Should Shake", false);
      }
    }
  }
}