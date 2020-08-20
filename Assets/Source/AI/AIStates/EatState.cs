using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : AIState
{
    public EatState(AIController ai) : base(ai)
    {
        Name = "EatState";
    }

    public override void Enter()
    {
        MyAI.MyNavMeshAgent.destination = MyAI.transform.position;
        base.Enter();
    }

    public override AIState EvaluateState()
    {
        if (MyAI.Health == 100f)
            return new WanderState(MyAI);

        float distance = Math.Abs(Vector3.Distance(MyAI.transform.position, Game.Instance.MyPlayer.transform.position));

        if (MyAI.Health < 75f && distance <= 100f) // And if distance to player is lesser than X
            return new FleeState(MyAI);

        if (MyAI.Health == 0)
            return new DeadState(MyAI);

        return base.EvaluateState();
    }

    public override void Update()
    {
        MyAI.RegenHealth(Time.deltaTime * 1.5f);
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
