using System.Collections;
using UnityEngine;


public class AimState : AimBaseState
{

    public override void EnterState(AimStateManager aim)
    {
        aim.CharacterAnimator.SetBool("Aiming", true);
        aim.CurrentFov = aim.ADSFov;
        aim.Crosshair.SetActive(true);
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Hip);
        }
    }
}
