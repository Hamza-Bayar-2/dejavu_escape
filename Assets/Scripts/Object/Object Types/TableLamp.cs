
using UnityEngine;
using UnityEngine.InputSystem;

public class TableLamp : ObjectInteraction
{
  [SerializeField] private Light lampLight;
  [SerializeField] private AudioSource lampAudio;
  [SerializeField] private AudioClip switchOn;
  [SerializeField] private AudioClip switchOff;
  private bool isLightOn = true;

  void Start()
  {
    if (lampLight == null)
    {
      Debug.LogError("No Light component found on " + gameObject.name + " or its children!");
      return;
    }

    // Başlangıç durumunu ayarla
    lampLight.enabled = isLightOn;
  }

  public void OnInteract(InputValue value)
  {
    if (InsideRange && value.isPressed && lampLight != null)
    {
      ToggleLight();
    }
  }

  private void ToggleLight()
  {
    isLightOn = !isLightOn;
    lampLight.enabled = isLightOn;
    PlaySound(isLightOn);
    
    Debug.Log("Table lamp " + (isLightOn ? "turned on" : "turned off"));
  }

  private void PlaySound(bool isOn)
  {
    if (isOn)
    {
      lampAudio.PlayOneShot(switchOn);
    }
    else
    {
      lampAudio.PlayOneShot(switchOff);
    }
  }
}