using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ActivityState
{
    public IdleState(Player player) : base(player)
    {
        Name = "IdleState";
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override PlayerState HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
            if (MyPlayer.MyMovementState.Name == "StandingState" && MyPlayer.MyEquippedState.Name == "UnarmedState")
                return new InventoryState(MyPlayer);

        return base.HandleInput();
    }

    public override void Update()
    {
        base.Update();
    }
}
