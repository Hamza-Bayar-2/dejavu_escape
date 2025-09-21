using UnityEngine;
using UnityEngine.InputSystem;

public class Drawer : ObjectInteraction
{
  [SerializeField] AudioSource drawerAudio;
  private Animator drawerAnimator;
  private bool isOpen;

  void Start()
  {
    drawerAnimator = GetComponent<Animator>();
    if (drawerAnimator == null)
    {
      drawerAnimator = GetComponentInChildren<Animator>();
    }
  }

  void Update()
  {
    HandleDrawerState();
  }

  public void OnInteract(InputValue value)
  {
    if (InsideRange && value.isPressed)
    {
      ToggleDrawer();
    }
  }

  private void ToggleDrawer()
  {
    isOpen = !isOpen;
    Debug.Log("Drawer " + (isOpen ? "opened" : "closed"));
  }

  private void HandleDrawerState()
  {
    if (drawerAnimator == null || !InsideRange)
    {
      return;
    }

    // It is enouph to check just one of the Drawer if it is open or not
    if (isOpen)
    {
      if (!drawerAnimator.GetBool("Is Drawer Open"))
      {
        drawerAnimator.SetBool("Is Drawer Open", true);
      }

    }
    else if (!isOpen)
    {
      if (drawerAnimator.GetBool("Is Drawer Open"))
      {
        drawerAnimator.SetBool("Is Drawer Open", false);
      }
    }
  }

  public void PlayDrawerSound()
  {
    drawerAudio.Play();
  }
}