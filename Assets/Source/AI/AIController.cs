using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float Health { get; private set; }

    public float AreaDeltaX { get; private set; }
    public float AreaDeltaZ { get; private set; }

    public Player PlayerActor { get; private set; }
    public AIState MyState { get; private set; }
    public ResourceHandler MyResHandler { get; private set; }
    public NavMeshAgent MyNavMeshAgent { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Health = 100;

        AreaDeltaX = 350f;
        AreaDeltaZ = 350f;

        MyResHandler = GetComponent<ResourceHandler>();
        MyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerActor = Game.Instance.MyPlayer;
        MyState = new WanderState(this);
        MyState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        AIState myAIState = MyState.EvaluateState();
        if(myAIState != null)
        {
            MyState.Exit();
            MyState = myAIState;
            MyState.Enter();
        }
        MyState.Update();
    }

    public void DrainHealth(float value)
    {
        Health -= value;
        if (Health < 0)
            Health = 0;
    }
    public void RegenHealth(float value)
    {
        Health += value;
        if (Health > 100)
            Health = 100;
    }

    public void GetShot()
    {
        DrainHealth(35);
    }
}
