using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : ActivityState
{
    public BuildingState(Player player) : base(player)
    {
        Name = "BuildingState";
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
