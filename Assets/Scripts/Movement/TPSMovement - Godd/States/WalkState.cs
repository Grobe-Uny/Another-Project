using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.characterAnimator.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ExitState(movement, movement.Crouch);
        } else if (movement.direction.magnitude < 0.1f)
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
        movement.characterAnimator.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
