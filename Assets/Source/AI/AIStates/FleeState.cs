using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : AIState
{
    public FleeState(AIController ai) : base(ai)
    {
        Name = "FleeState";
    }

    public override void Enter()
    {
        float newPositionX = Game.Instance.MyPlayer.transform.position.x + MyAI.AreaDeltaX;
        float newPositionZ = Game.Instance.MyPlayer.transform.position.z + MyAI.AreaDeltaZ;
        float newPositionY = Environment.Instance.GameMap.SampleHeight(new Vector3(newPositionX, 0, newPositionZ));

        MyAI.MyNavMeshAgent.destination = new Vector3(newPositionX, newPositionY, newPositionZ);

        base.Enter();
    }

    public override AIState EvaluateState()
    {
        if (MyAI.Health == 100f)
            return new WanderState(MyAI);

        float distance = Math.Abs(Vector3.Distance(MyAI.transform.position, Game.Instance.MyPlayer.transform.position));
        
        if (MyAI.Health < 75f && distance > 100f)
            return new EatState(MyAI);

        if (MyAI.Health == 0)
            return new DeadState(MyAI);

        return base.EvaluateState();
    }

    public override void Update()
    {       
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
