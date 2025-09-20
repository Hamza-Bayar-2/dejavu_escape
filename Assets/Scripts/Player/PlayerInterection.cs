using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI pressEText;

    private void Start()
    {
        // Make sure pressEText is initially hidden
        if (pressEText != null)
        {
            pressEText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has PlayerInput component
        PlayerInput playerInput = other.GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            // Show Press E text
            if (pressEText != null)
            {
                pressEText.gameObject.SetActive(true);
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that exited the trigger has PlayerInput component
        PlayerInput playerInput = other.GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            // Hide Press E text
            if (pressEText != null)
            {
                pressEText.gameObject.SetActive(false);
            }
        }
    }
}