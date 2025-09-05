using System.Collections;
using UnityEngine;


public class CrouchState : MovementBaseState
{

    public override void EnterState(MovementStateManager movement)
    {
        movement.characterAnimator.SetBool("Crouching", true);   
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
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.characterAnimator.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}