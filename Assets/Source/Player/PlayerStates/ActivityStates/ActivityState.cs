using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityState : PlayerState
{
    public ActivityState(Player player) : base(player)
    {
        Name = "ActivityState";
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override PlayerState HandleInput()
    {
        return null;
    }

    public override void Update()
    {
        
    }
}
