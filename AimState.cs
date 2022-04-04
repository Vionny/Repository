using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void Enter(AimStateManager aim)
    {
        aim.animate.SetBool("Aim",true);

    }
    public override void Update(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
