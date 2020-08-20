using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestingState : ActivityState
{
    public HarvestingState(Player player) : base(player)
    {
        Name = "HarvestingState";
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
        return base.HandleInput();
    }

    public override void Update()
    {
        base.Update();
    }
}
