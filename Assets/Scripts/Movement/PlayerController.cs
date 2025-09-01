using UnityEngine;

/// <summary>
/// A third-person character controller using a Rigidbody.
/// Movement is relative to the camera and the character turns to face the movement direction.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The speed at which the character moves.")]
    public float moveSpeed = 5f;

    [Tooltip("The speed at which the character turns to face the movement direction.")]
    public float turnSpeed = 10f;

    private Rigidbody rb;
    private Transform mainCameraTransform;
    private Vector3 moveDirection;

    public Animator playerAnimator;

    void Awake()
    {
        // Get the Rigidbody component attached to this GameObject.
        rb = GetComponent<Rigidbody>();
        
        // Find the main camera in the scene and get its transform.
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("PlayerController: Main Camera not found in the scene! Please ensure you have a camera tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        // --- Input Handling ---
        // Read input from the horizontal and vertical axes (W, A, S, D keys or gamepad stick).
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Store the input in a Vector3.
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        
        // Update animator parameters for blend tree
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Horizontal", horizontalInput);
            playerAnimator.SetFloat("Vertical", verticalInput);
        }
    }

    void FixedUpdate()
    {
        // --- Movement and Rotation Logic ---
        // We run physics-related code in FixedUpdate for consistency.
        
        // If there is no input, do nothing.
        if (moveDirection.magnitude < 0.1f)
        {
            return;
        }

        // Calculate the movement direction relative to the camera's orientation.
        Vector3 targetDirection = CalculateCameraRelativeMovement();

        // Rotate the character to face the target direction.
        RotateCharacter(targetDirection);

        // Move the character.
        MoveCharacter(targetDirection);
    }

    private Vector3 CalculateCameraRelativeMovement()
    {
        // Get the forward and right vectors of the camera, but ignore the vertical (y) component.
        Vector3 cameraForward = mainCameraTransform.forward;
        Vector3 cameraRight = mainCameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Create the desired movement direction relative to the camera.
        return (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized;
    }
    
    private void RotateCharacter(Vector3 targetDirection)
    {
        // Create a rotation that looks in the target direction.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Smoothly interpolate from the current rotation to the target rotation.
        Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);

        // Apply the new rotation to the Rigidbody.
        rb.MoveRotation(newRotation);
    }

    private void MoveCharacter(Vector3 targetDirection)
    {
        // Calculate the movement step.
        Vector3 movement = targetDirection * moveSpeed * Time.fixedDeltaTime;

        // Apply the movement to the Rigidbody's position.
        rb.MovePosition(rb.position + movement);
  
    }
}
