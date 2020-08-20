using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState
{
    public AIController MyAI { get; protected set; }
    public string Name { get; protected set; }

    public AIState(AIController ai)
    {
        MyAI = ai;
        Name = "AIState";
    }

    public virtual void Enter()
    {
        
    }

    public virtual AIState EvaluateState()
    {
        return null;
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        MyAI.MyNavMeshAgent.destination = MyAI.transform.position;
    }
}
