using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpStrength = 5;
    [SerializeField] float jumbStartLimit = 0.01f;
    [SerializeField] float mouseSensitivity = 10f;
    [SerializeField] float crouchHight = 0.5f;
    [SerializeField] float crouchSpeed = 0.3f; // Movement speed multiplier when crouching
    [SerializeField] float crouchTransitionSpeed = 5f; // How fast to crouch/stand
    [SerializeField] private Camera myCamera;

    private Rigidbody rb;
    private Vector2 movement;
    private Vector2 mouseInput;
    private float xRotation = 0f;
    private Vector3 cameraStartPosition;
    private Vector3 targetCameraPosition;
    private bool isCrouching = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraStartPosition = myCamera.transform.localPosition;
        targetCameraPosition = cameraStartPosition;
    }

    void Update()
    {
        Move();
        RotateWithMouse();
        RotateCameraUpDown();
        HandleCrouchTransition();
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

    public void OnCrouch(InputValue value)
    {
        Crouch(value);
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 worldMoveDirection = transform.TransformDirection(moveDirection);

        // Apply speed multiplier when crouching
        float currentSpeed = isCrouching ? moveSpeed * crouchSpeed : moveSpeed;

        transform.position += worldMoveDirection * currentSpeed * Time.deltaTime;
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

    private void Crouch(InputValue value)
    {
        if (value.isPressed)
        {
            isCrouching = true;
            targetCameraPosition = new Vector3(cameraStartPosition.x,
                                             cameraStartPosition.y - crouchHight,
                                             cameraStartPosition.z);
            Debug.Log("Player crouching");
        }
        else
        {
            isCrouching = false;
            targetCameraPosition = cameraStartPosition;
            Debug.Log("Player standing up");
        }
    }

    private void HandleCrouchTransition()
    {
        // Smoothly lerp camera position to target
        myCamera.transform.localPosition = Vector3.Lerp(
            myCamera.transform.localPosition,
            targetCameraPosition,
            crouchTransitionSpeed * Time.deltaTime
        );
    }
}

