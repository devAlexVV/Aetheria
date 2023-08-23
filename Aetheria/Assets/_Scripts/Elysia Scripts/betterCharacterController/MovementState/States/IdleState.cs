using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(BetterPlayerMovement movement)
    {

    }

    public override void UpdateState(BetterPlayerMovement movement)
    {

        if (movement.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))movement.SwitchState(movement.Run);
            else movement.SwitchState(movement.Walk);
        }
        if (Input.GetKeyDown(KeyCode.C))movement.SwitchState(movement.Crouch);

    }
}
