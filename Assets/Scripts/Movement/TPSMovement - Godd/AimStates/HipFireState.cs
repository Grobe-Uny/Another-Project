using System.Collections;
using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.CharacterAnimator.SetBool("Aiming",false);
        aim.CurrentFov = aim.HipFov;
        aim.Crosshair.SetActive(false);
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Aim);
        }
    }
}
