using UnityEngine;

class ObjectInteraction : MonoBehaviour
{
  private Material myMaterial;

  void Awake()
  {
    // Get material from the renderer component
    myMaterial = GetComponent<Renderer>().material;

    // Disable emission
    myMaterial.DisableKeyword("_EMISSION");
  }

  void OnTriggerEnter(Collider other)
  {
    Glow(other);
  }

  void OnTriggerExit(Collider other)
  {
    StopGlow(other);
  }

  private void Glow(Collider other)
  {
    if (other.gameObject.CompareTag("Crosshair"))
    {
      myMaterial.EnableKeyword("_EMISSION");
      Debug.Log("Glow");
    }
  }

  private void StopGlow(Collider other)
  {
    if (other.gameObject.CompareTag("Crosshair"))
    {
      myMaterial.DisableKeyword("_EMISSION");
      Debug.Log("StopGlow");
    }
  }


}