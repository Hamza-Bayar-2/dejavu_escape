using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetObject : ObjectInteraction
{
  [SerializeField] ParticleSystem targetFoundPS;
  public static bool isTargetFound = false;
  private static bool isTargetVisible = true;

  // Event that fires when target is found
  public static event Action OnTargetFound;

  public void OnAttack(InputValue value)
  {
    if (InsideRange && value.isPressed && !isTargetFound && isTargetVisible)
    {
      Instantiate(targetFoundPS, this.transform.localPosition, this.transform.localRotation);
      isTargetFound = true;

      // Fire event when target is found
      OnTargetFound?.Invoke();

      Debug.Log("Target found! Event fired.");
    }
  }

  public static void SetTargetVisibility(bool isVisible)
  {
    isTargetVisible = isVisible;
  }
}