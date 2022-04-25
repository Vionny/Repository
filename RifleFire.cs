using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFire : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.animate.SetBool("Aim", false);
        if (Input.GetKeyDown(KeyCode.Mouse1)) aim.currentFOV = aim.adsFOV;
    }
    public override void UpdateState(AimStateManager aim)
    {
        if(Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
