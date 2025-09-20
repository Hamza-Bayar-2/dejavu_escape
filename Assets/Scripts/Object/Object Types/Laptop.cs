using UnityEngine;
using UnityEngine.InputSystem;

public class Laptop : ObjectInteraction
{
  private Animator LaptopAnimator;
  private bool isOpen;

  void Start()
  {
    LaptopAnimator = GetComponent<Animator>();
    if (LaptopAnimator == null)
    {
      LaptopAnimator = GetComponentInChildren<Animator>();
    }
  }

  void Update()
  {
    HandleLaptopState();
  }

  public void OnInteract(InputValue value)
  {
    if (InsideRange && value.isPressed)
    {
      ToggleLaptop();
    }
  }

  private void ToggleLaptop()
  {
    isOpen = !isOpen;
    Debug.Log("Laptop " + (isOpen ? "opened" : "closed"));
  }

  private void HandleLaptopState()
  {
    if (LaptopAnimator == null || !InsideRange)
    {
      return;
    }

    // It is enouph to check just one of the Laptop if it is open or not
    if (isOpen)
    {
      if (!LaptopAnimator.GetBool("Is Open"))
      {
        LaptopAnimator.SetBool("Is Open", true);
      }

    }
    else if (!isOpen)
    {
      if (LaptopAnimator.GetBool("Is Open"))
      {
        LaptopAnimator.SetBool("Is Open", false);
      }
    }
  }
}