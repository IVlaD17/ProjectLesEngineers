using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : AIState
{
    public WanderState(AIController ai) : base(ai)
    {
        Name = "WanderState";
    }

    public override void Enter()
    {
        float newPositionX = UnityEngine.Random.Range(Game.Instance.MyPlayer.transform.position.x - MyAI.AreaDeltaX, Game.Instance.MyPlayer.transform.position.x + MyAI.AreaDeltaX);
        float newPositionZ = UnityEngine.Random.Range(Game.Instance.MyPlayer.transform.position.z - MyAI.AreaDeltaZ, Game.Instance.MyPlayer.transform.position.z + MyAI.AreaDeltaZ);
        float newPositionY = Environment.Instance.GameMap.SampleHeight(new Vector3(newPositionX, 0, newPositionZ));

        MyAI.MyNavMeshAgent.destination = new Vector3(newPositionX, newPositionY, newPositionZ);
        base.Enter();
    }

    public override AIState EvaluateState()
    {
        float distance = Math.Abs(Vector3.Distance(MyAI.transform.position, Game.Instance.MyPlayer.transform.position));

        if (MyAI.Health < 75f && distance <= 100f) // And if distance to player is lesser than X
            return new FleeState(MyAI);

        if (MyAI.Health < 75f && distance > 100f) // And if distance to player is greater than X
            return new EatState(MyAI);

        if (MyAI.Health == 0)
            return new DeadState(MyAI);

        return base.EvaluateState();
    }

    public override void Update()
    {
        if(Vector3.Distance(MyAI.transform.position, MyAI.MyNavMeshAgent.destination) < 5f)
        {
            float newPositionX = UnityEngine.Random.Range(Game.Instance.MyPlayer.transform.position.x - MyAI.AreaDeltaX, Game.Instance.MyPlayer.transform.position.x + MyAI.AreaDeltaX);
            float newPositionZ = UnityEngine.Random.Range(Game.Instance.MyPlayer.transform.position.z - MyAI.AreaDeltaZ, Game.Instance.MyPlayer.transform.position.z + MyAI.AreaDeltaZ);
            float newPositionY = Environment.Instance.GameMap.SampleHeight(new Vector3(newPositionX, 0, newPositionZ));

            MyAI.MyNavMeshAgent.destination = new Vector3(newPositionX, newPositionY, newPositionZ);
        }

        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}