using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpStrength = 5;

    private Rigidbody rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 offset = movement * moveSpeed * Time.deltaTime;
        Vector3 myLocalPosition = transform.localPosition; // Fixed typo

        // Eksenleri değiştirdik: Z ekseni = sağ/sol, X ekseni = ileri/geri
        transform.localPosition = new Vector3(myLocalPosition.x + offset.y, myLocalPosition.y, myLocalPosition.z + offset.x);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.01f) // Only jump if grounded
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log(movement);
    }
}
