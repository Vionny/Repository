using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFire : AimBaseState
{
    public override void Enter(AimStateManager aim)
    {
        aim.animate.SetBool("Aim", false);
        if (Input.GetKeyUp(KeyCode.Mouse2)) aim.currentFOV = aim.adsFOV;
    }
    public override void Update(AimStateManager aim)
    {
        if(Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
