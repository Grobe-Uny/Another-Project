using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Third-person controller s potpunim root motionom.
/// Animacija upravlja kretanjem, a skripta skalira korake (za brže hodanje/trčanje)
/// i rotira lika prema inputu.
/// </summary>
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementScriptv2 : MonoBehaviour
{
    [Header("Debug")]
    public TextMeshProUGUI currentVelocityText;

    [Header("Movement Settings")]
    [Tooltip("Multiplikator root motion brzine (1 = točno kao u animaciji, >1 = brže, <1 = sporije).")]
    public float rootMotionSpeedMultiplier = 1.4f;

    [Tooltip("Brzina rotacije lika prema smjeru inputa.")]
    public float turnSpeed = 15f;

    [Tooltip("Koliko brzo lik reagira na promjenu inputa (za blend tree).")]
    public float inputResponsiveness = 8f;

    [Header("Dependencies")]
    public Animator playerAnimator;

    private CharacterController characterController;
    private Transform mainCameraTransform;

    // Input
    private Vector2 playerInput;

    // Animator parameter hashes
    private readonly int velocityXHash = Animator.StringToHash("velocityX");
    private readonly int velocityZHash = Animator.StringToHash("velocityZ");
    private readonly int speedHash = Animator.StringToHash("speed");

    private float verticalVelocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (playerAnimator == null)
            playerAnimator = GetComponent<Animator>();

        if (Camera.main != null)
            mainCameraTransform = Camera.main.transform;
        else
            Debug.LogError("Main Camera not found! Tag your camera as 'MainCamera'.");
    }

    void Update()
    {
        HandleInput();
        HandleRotation();
        HandleAnimation();

        if (currentVelocityText != null)
            currentVelocityText.text = $"Current velocity: {characterController.velocity.magnitude:F2} m/s";
    }

    private void HandleInput()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
    }

    private void HandleRotation()
    {
        // Ako nema inputa, ne rotiraj
        if (playerInput.magnitude < 0.1f) return;

        // Smjer kretanja prema kameri
        Vector3 cameraForward = mainCameraTransform.forward;
        Vector3 cameraRight = mainCameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 desiredDirection = (cameraForward * playerInput.y + cameraRight * playerInput.x).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void HandleAnimation()
    {
        // Brzina inputa za blend tree (smooth)
        float inputMagnitude = Mathf.Clamp01(playerInput.magnitude);
        float smoothedSpeed = Mathf.Lerp(playerAnimator.GetFloat(speedHash), inputMagnitude, inputResponsiveness * Time.deltaTime);

        playerAnimator.SetFloat(speedHash, smoothedSpeed);
        playerAnimator.SetFloat(velocityXHash, playerInput.x);
        playerAnimator.SetFloat(velocityZHash, playerInput.y);
    }

    void OnAnimatorMove()
    {
        if (playerAnimator == null || characterController == null) return;

        // Root motion delta
        Vector3 delta = playerAnimator.deltaPosition * rootMotionSpeedMultiplier;

        // Dodaj gravitaciju
        if (characterController.isGrounded)
            verticalVelocity = -1f;
        else
            verticalVelocity += Physics.gravity.y * Time.deltaTime;

        delta.y = verticalVelocity * Time.deltaTime;

        // Primijeni pomak
        characterController.Move(delta);
    }
}