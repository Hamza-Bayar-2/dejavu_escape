using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetObject : ObjectInteraction
{
  [SerializeField] ParticleSystem targetFoundPS;
  private bool isTargetFound = false;
  
  // Event that fires when target is found
  public static event Action OnTargetFound;
  
  public bool IsTargetFound
  {
    get
    {
      return isTargetFound;
    }
  }
  public void OnAttack(InputValue value)
  {
    if (InsideRange && value.isPressed && !isTargetFound)
    {
      Instantiate(targetFoundPS, this.transform.localPosition, this.transform.localRotation);
      isTargetFound = true;
      
      // Fire event when target is found
      OnTargetFound?.Invoke();
      
      Debug.Log("Target found! Event fired.");
    }
  }
}