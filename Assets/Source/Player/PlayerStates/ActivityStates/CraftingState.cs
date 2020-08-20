using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingState : ActivityState
{
    public CraftingState(Player player) : base(player)
    {
        Name = "CraftingState";
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
