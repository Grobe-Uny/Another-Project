using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MovementStateManager : MonoBehaviour
{

    [Header("Various Script References")]
    //public PlayerStats playerStats;
    public Animator characterAnimator;
    public Rig rig1;
    public CharacterController characterController;

    public Vector3 direction;
    public float hzInput, vInput;

    public GameObject HandHolder;

    public KeyCode SprintKey = KeyCode.LeftShift;

    public Vector3 SpherePosition;

    #region Gravity related
    public float gravity = -9.81f;
    public float JumpForce;
    public bool Jumped;
    public Vector3 Velocity;
    #endregion
    bool isWeaponEquiped;

    #region MovementStates
    public MovementBaseState previousState;
    public MovementBaseState CurrentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public CrouchState Crouch = new CrouchState();
    public JumpState Jump = new JumpState();

    #endregion
    
    void Start()
    {
        if (characterAnimator == null)
        { 
            characterController = GetComponent<CharacterController>();
        }
        SwitchState(Idle);
        isWeaponEquiped = false;
    }

    // Update is called once per frame
    void Update()
    {           
        GetDirectionAndMove();
        GravityCalculation();
        
        characterAnimator.SetFloat("X", hzInput);
        characterAnimator.SetFloat("Y", vInput);
        
        CurrentState.UpdateState(this);

        if (HandHolder.GetComponentInChildren<WeaponManager>())
        {
            isWeaponEquiped = true;
        }
        else
        {
            isWeaponEquiped = false;
        }
        
        if (!isWeaponEquiped)
        {
            rig1.weight = 0;
            characterAnimator.SetLayerWeight(1, 0);
        }else if (isWeaponEquiped)
        {
            rig1.weight = 1;
            characterAnimator.SetLayerWeight(1,1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(SpherePosition, characterController.radius- 0.1f);
    }

    public void SwitchState(MovementBaseState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }
    /*public bool isGrounded()
    {
        //SpherePosition = new Vector3(transform.position.x, transform.position.y - GroundYOffset, transform.position.z);
        /*SpherePosition = new Vector3(transform.position.x, transform.position.y + characterController.radius - 0.08f, transform.position.z);
        if (Physics.CheckSphere(SpherePosition, characterController.radius - 0.05f, GroundMask)) return true;
        return false;
        SpherePosition = new Vector3(transform.position.x, transform.position.y - GroundYOffset, transform.position.z);
        bool grounded = Physics.CheckSphere(SpherePosition, characterController.radius - 0.1f, GroundMask);
        //Debug.Log("isGrounded: " + grounded + " at position " + SpherePosition);
        return grounded;
    }*/

    void GravityCalculation()
    {
        if (!characterController.isGrounded)
        {
            Velocity.y += gravity * Time.deltaTime;
        }
        else if (Velocity.y < 0)
        {
            Velocity.y = -2;
        }

        characterController.Move(Velocity * Time.deltaTime);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        direction = transform.forward * vInput + transform.right * hzInput;

        if (Input.GetKey(SprintKey))
        {
           // characterController.Move(direction.normalized * playerStats.RunningSpeedStat.BaseValue * Time.deltaTime);
        }
        else
        {
            //characterController.Move(direction.normalized * playerStats.WalkingSpeedStat.BaseValue * Time.deltaTime);
        }
    }
    public void JumpingForce() => Velocity.y += JumpForce;

    public void jumped() => Jumped = true;

    public void JumpFinished() => Jumped = false;

}
