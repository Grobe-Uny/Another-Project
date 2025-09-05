using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.JumpingForce();
        if(movement.previousState == movement.Idle)
        {
            movement.characterAnimator.SetTrigger("IdleJump");
        }
        else if (movement.previousState == movement.Walk || movement.previousState == movement.Run)
        {
            movement.characterAnimator.SetTrigger("RunJump");
        }
    }
    public override void UpdateState(MovementStateManager movement)
    {
        
        if(!movement.Jumped && movement.characterController.isGrounded)
        {
            Debug.Log("Character grounded, exiting JumpState");
            movement.Velocity.y = 0f; 
            if (movement.hzInput == 0 && movement.vInput == 0) 
            {
                ExitState(movement, movement.Idle);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                ExitState(movement, movement.Run);
            }
            else if (movement.hzInput > 0.1f || movement.vInput > 0.1f)
            {
                ExitState(movement, movement.Walk);
            }
        }
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.SwitchState(state);
    }
}
