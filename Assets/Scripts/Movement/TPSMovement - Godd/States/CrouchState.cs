using System.Collections;
using UnityEngine;


public class CrouchState : MovementBaseState
{

    public override void EnterState(MovementStateManager movement)
    {
        movement.characterAnimator.SetBool("isCrouching", true);   
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }else if (Input.GetKeyDown(KeyCode.C))
        {
            if(movement.direction.magnitude < 0.1f)
            {
                ExitState(movement, movement.Idle);
            }else
            {
                ExitState(movement, movement.Walk);
            }
        }
        if(movement.vInput < 0)
        {
            movement.currentMoveSpeed = movement.CrouchBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.CrouchSpeed;
        }
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.characterAnimator.SetBool("isCrouching", false);
        movement.SwitchState(state);
    }
}