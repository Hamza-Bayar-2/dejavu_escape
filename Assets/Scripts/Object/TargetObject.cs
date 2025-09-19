using UnityEngine;
using UnityEngine.InputSystem;

public class TargetObject : ObjectInteraction
{
  [SerializeField] ParticleSystem targetFoundPS;
  private bool isTargetFound = false;

  public void OnAttack(InputValue value)
  {
    if (InsideRange && value.isPressed && !isTargetFound)
    {
      Instantiate(targetFoundPS, this.transform.localPosition, this.transform.localRotation);
      isTargetFound = true;
    }
  }
}