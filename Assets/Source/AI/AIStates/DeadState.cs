using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : AIState
{
    public DeadState(AIController ai) : base(ai)
    {
        Name = "DeadState";
    }

    public override void Enter()
    {
        MyAI.MyResHandler.Harvest();
        base.Enter();
    }
    public override AIState EvaluateState()
    {
        return new WanderState(MyAI);
    }

    public override void Update()
    {
        MyAI.MyNavMeshAgent.Warp(Vector3.zero);
        base.Update();
    }

    public override void Exit()
    {
        MyAI.RegenHealth(100);
        base.Exit();
    }
}
