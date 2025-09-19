using UnityEngine;

class ObjectInteraction : MonoBehaviour
{
  private Material[] myMaterials;
  private Renderer[] myRenderers;
  private Color[] originalColors;
  [SerializeField] private float brightenAmount = 0.3f; // Ne kadar beyazlaştırılacak (0-1 arası)

  void Awake()
  {
    // Get all renderers from this object and its children
    myRenderers = GetComponentsInChildren<Renderer>();

    // If no renderers found, log error
    if (myRenderers.Length == 0)
    {
      Debug.LogError("No Renderers found on " + gameObject.name + " or its children!");
      return;
    }

    // Collect all materials from all renderers
    System.Collections.Generic.List<Material> allMaterials = new System.Collections.Generic.List<Material>();

    for (int r = 0; r < myRenderers.Length; r++)
    {
      Material[] rendererMaterials = myRenderers[r].materials;
      for (int m = 0; m < rendererMaterials.Length; m++)
      {
        allMaterials.Add(rendererMaterials[m]);
      }
    }

    myMaterials = allMaterials.ToArray();
    originalColors = new Color[myMaterials.Length];

    // Store original colors
    for (int i = 0; i < myMaterials.Length; i++)
    {
      originalColors[i] = myMaterials[i].color;
    }

    Debug.Log("Found " + myRenderers.Length + " Renderers with total " + myMaterials.Length + " materials");
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