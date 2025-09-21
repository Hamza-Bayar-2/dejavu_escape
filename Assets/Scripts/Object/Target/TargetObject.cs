using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetObject : ObjectInteraction
{
  [SerializeField] ParticleSystem targetFoundPS;
  [SerializeField] private AudioSource targetFoundAudio;
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

      PlaySound();

      // Fire event when target is found
      OnTargetFound?.Invoke();

      Debug.Log("Target found! Event fired.");
    }
  }

  void OnDestroy()
  {
    isTargetFound = false;
  }

  private void PlaySound()
  {
    // Play target found sound
    if (targetFoundAudio != null)
    {
      targetFoundAudio.Play();
    }
  }

  public static void SetTargetVisibility(bool isVisible)
  {
    isTargetVisible = isVisible;
  }
}