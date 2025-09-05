using System.Collections;
using UnityEngine;


public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.characterAnimator.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Walk);
        }else if (movement.direction.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.Jump);
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.characterAnimator.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
