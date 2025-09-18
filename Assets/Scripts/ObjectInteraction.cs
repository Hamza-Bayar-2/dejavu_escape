using UnityEngine;

class ObjectInteraction : MonoBehaviour
{
  private Material[] myMaterials;
  private Renderer myRenderer;
  private Color[] originalColors;
  [SerializeField] private float brightenAmount = 0.3f; // Ne kadar beyazlaştırılacak (0-1 arası)

  void Awake()
  {
    // First try to get renderer from this object
    myRenderer = GetComponent<Renderer>();

    // If not found, look in children
    if (myRenderer == null)
    {
      myRenderer = GetComponentInChildren<Renderer>();
    }

    // If still not found, log error
    if (myRenderer == null)
    {
      Debug.LogError("No Renderer found on " + gameObject.name + " or its children!");
      return;
    }

    myMaterials = myRenderer.materials;
    originalColors = new Color[myMaterials.Length];

    // Store original colors
    for (int i = 0; i < myMaterials.Length; i++)
    {
      originalColors[i] = myMaterials[i].color;
    }

    Debug.Log("Found Renderer on: " + myRenderer.gameObject.name + " with " + myMaterials.Length + " materials");
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
    if (other.gameObject.CompareTag("Crosshair") && myMaterials != null)
    {
      // Brighten colors for all materials
      for (int i = 0; i < myMaterials.Length; i++)
      {
        Color brightenedColor = Color.Lerp(originalColors[i], Color.white, brightenAmount);
        myMaterials[i].color = brightenedColor;
      }
      Debug.Log("Glow - Brightened colors for " + myMaterials.Length + " materials");
    }
  }

  private void StopGlow(Collider other)
  {
    if (other.gameObject.CompareTag("Crosshair") && myMaterials != null)
    {
      // Restore original colors for all materials
      for (int i = 0; i < myMaterials.Length; i++)
      {
        myMaterials[i].color = originalColors[i];
      }
      Debug.Log("StopGlow - Restored original colors for " + myMaterials.Length + " materials");
    }
  }
}