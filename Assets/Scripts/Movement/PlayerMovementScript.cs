using System;
using Benetti;
using TMPro;
using UnityEngine;

/// <summary>
/// A third-person character controller inspired by GTA V.
/// It uses a CharacterController for smooth movement with acceleration and deceleration.
/// Animation is driven by the character's actual velocity, not raw input.
/// </summary>
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementScript : MonoBehaviour
{
    [Header("Testing stuff")]
    public TextMeshProUGUI currentVelocitytext;
    public TextMeshProUGUI currentGameStateText;
    
    
    [Header("Movement Settings")]
    [Tooltip("The maximum speed of the character.")]
    public float maxSpeed = 8f;
    public float sprintMaxSpeed = 12f;

    [Tooltip("How quickly the character reaches max speed.")]
    public float acceleration = 10f;

    [Tooltip("How quickly the character stops when there is no input.")]
    public float deceleration = 15f;

    [Tooltip("The speed at which the character turns to face the movement direction.")]
    public float turnSpeed = 15f;

    [Tooltip("The force applied when the character jumps.")]
    public float jumpForce = 5f;
    
    [Header("Dependencies")]
    [Tooltip("The Animator component for the character.")]
    public Animator playerAnimator;

    private CharacterController characterController;
    private Transform mainCameraTransform;

    // Internal state
    private Vector3 currentVelocity;
    private Vector3 targetVelocity;
    private Vector2 playerInput;
    private float verticalVelocity; // Used for gravity
    private float targetSpeed; // Target speed based on input

    // Animator parameter hashes for performance
    private readonly int velocityXHash = Animator.StringToHash("velocityX");
    private readonly int velocityZHash = Animator.StringToHash("velocityZ");

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
        
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("PlayerController: Main Camera not found. Please tag your main camera with 'MainCamera'.");
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
       /* // Ako si trenutno u Jump animaciji
        AnimatorStateInfo state = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Jump"))
        {
            // Kad animacija završi (normalizedTime >= 1.0f) i lik je na tlu
            if (state.normalizedTime >= 1.0f && characterController.isGrounded)
            {
                playerAnimator.SetBool("isJumping", false);
            }
        }*/
        HandleInput();
        HandleMovement();
        HandleAnimation();
        currentVelocitytext.text = "Current velocity: " + currentVelocity.ToString();
        currentGameStateText.text = "Current game state: " + GameStateManager.CurrentState.ToString();
    } 

    private void HandleInput()
    {
        // Read input from WASD keys or a gamepad
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
       /* if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            verticalVelocity = jumpForce; // pokreni skok
            playerAnimator.SetBool("isJumpingIdle", true);
        }*/
    }

    private void HandleMovement()
    {
        // Calculate the movement direction relative to the camera
        Vector3 cameraForward = mainCameraTransform.forward;
        Vector3 cameraRight = mainCameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 desiredMoveDirection = (cameraForward * playerInput.y + cameraRight * playerInput.x).normalized;

        // Calculate the target velocity
        if(Input.GetKey(KeyCode.LeftShift))
        {
             targetVelocity = desiredMoveDirection * sprintMaxSpeed;
             playerAnimator.SetBool("isSprinting", true);
        }
        else
        {
             targetVelocity = desiredMoveDirection * maxSpeed;
             playerAnimator.SetBool("isSprinting", false);
        }

        // Determine the rate of change for the velocity
        //float rateOfChange = (playerInput.magnitude > 0.1f) ? acceleration : deceleration;

        // Smoothly interpolate the current velocity towards the target velocity (acceleration/deceleration)
        //currentVelocity.x = Mathf.Lerp(currentVelocity.x, targetVelocity.x, rateOfChange * Time.deltaTime);
        //currentVelocity.z = Mathf.Lerp(currentVelocity.z, targetVelocity.z, rateOfChange * Time.deltaTime);
        
        
        // Determine the correct rate (acceleration or deceleration) to use
        float currentRate = (playerInput.magnitude > 0.1f) ? acceleration : deceleration;
        
        // Ako smo u StopWalking animaciji → pojačaj deceleraciju
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("StopWalking"))
        {
            currentRate = deceleration * 1.5f;
            targetVelocity = Vector3.zero; // cilj nam je zaustaviti se
        }
        
        // Use Vector3.MoveTowards for a frame-rate independent and smooth transition.
        // This is the key change that ensures smooth deceleration.
        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, currentRate * Time.deltaTime);

        // --- Rotation ---
        if (desiredMoveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // --- Gravity ---
        if (characterController.isGrounded)
        {
            // Reset vertical velocity if grounded
            verticalVelocity = -1f; 
        }
        else
        {
            // Apply gravity
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        playerAnimator.SetFloat("speed", currentVelocity.magnitude);
        // --- Apply Movement ---
        Vector3 finalMovement = currentVelocity * Time.deltaTime;
        finalMovement.y = verticalVelocity * Time.deltaTime;
        //characterController.Move(finalMovement);

        if (playerInput.x == 0 && playerInput.y == 0)
        {
            playerAnimator.SetBool("stopInput", true);
        }
        else
        {
            playerAnimator.SetBool("stopInput", false);
        }
    }

    void OnAnimatorMove()
    {
        if (playerAnimator == null || characterController == null) return;

        // Delta kretanja iz root motiona
        Vector3 rootDelta = playerAnimator.deltaPosition;

        // Smjer iz root motiona (world space)
        Vector3 rootDirection = rootDelta.normalized;

        // Koliko root motion želi pomaknuti (njegova brzina)
        float rootSpeed = playerAnimator.velocity.magnitude;

        // Kolika brzina treba biti prema inputu (maksimalna kada input.magnitude = 1)
        if(Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed = playerInput.magnitude * sprintMaxSpeed;
            playerAnimator.SetBool("isSprinting", true);
        }
        else
        {
            targetSpeed = playerInput.magnitude * maxSpeed;
            playerAnimator.SetBool("isSprinting", false);
        }
        // Glatko blendaj root speed prema ciljanom speedu
        float blendedSpeed = Mathf.Lerp(rootSpeed, targetSpeed, 0.5f); 
        // Možeš podešavati taj faktor (0.5f) – veći = više root motiona, manji = više input kontrole

        // Konačno kretanje
        Vector3 finalMove = rootDirection * blendedSpeed * Time.deltaTime;

        // Dodaj gravitaciju
        finalMove.y = verticalVelocity * Time.deltaTime;

        // Primijeni rotaciju iz animacije samo ako se krećeš, inače koristi input smjer
        if (playerInput.magnitude > 0.1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rootDirection), turnSpeed * Time.deltaTime);
        else
            transform.rotation *= playerAnimator.deltaRotation;

        // Pomakni karakter
        characterController.Move(finalMove);
                
        /*// Ako si u Jump stanju, koristi root motion Y
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            characterController.Move(finalMove.y * Vector3.up * jumpForce * Time.deltaTime);
        }*/


        // Spremi za debug
        currentVelocity = finalMove / Time.deltaTime;
    }

    private void HandleAnimation()
    {
        // Get the world-space velocity of the character controller
        Vector3 worldVelocity = characterController.velocity;

        // Transform the world velocity to the character's local space
        Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);
        
        // Remove the y component for animation purposes
        localVelocity.y = 0;

        // Send the local velocity components to the animator
        // These values will be used to drive a 2D blend tree
        playerAnimator.SetFloat(velocityXHash, localVelocity.x);
        playerAnimator.SetFloat(velocityZHash, localVelocity.z);
    }
}