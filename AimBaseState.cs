using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBaseState
{
    public abstract void Enter(AimStateManager aim);
    public abstract void Update(AimStateManager aim);
}
