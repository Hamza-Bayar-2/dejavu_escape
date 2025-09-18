using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpStrength = 5;
    [SerializeField] float jumbStartLimit = 0.01f;
    [SerializeField] float mouseSensitivity = 10f;
    [SerializeField] private Camera myCamera;

    private Rigidbody rb;
    private Vector2 movement;
    private Vector2 mouseInput;
    private float xRotation = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        RotateWithMouse();
        RotateCameraUpDown();
    }

    public void OnJump(InputValue value)
    {
        Jump(value);
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        mouseInput = value.Get<Vector2>();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 worldMoveDirection = transform.TransformDirection(moveDirection);

        transform.position += worldMoveDirection * moveSpeed * Time.deltaTime;
    }

    private void RotateWithMouse()
    {
        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;

        // Rotate the player around the Y-axis (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);
    }

    private void RotateCameraUpDown()
    {
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 90f);

        myCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Jump(InputValue value)
    {
        if (value.isPressed && Mathf.Abs(rb.linearVelocity.y) < jumbStartLimit)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }
}

