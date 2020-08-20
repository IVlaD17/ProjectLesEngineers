using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public Player MyPlayer { get; protected set; }
    public string Name { get; protected set; }

    public PlayerState(Player player)
    {
        MyPlayer = player;
        Name = "PlayerState";
    }
    public abstract void Enter();
    public abstract PlayerState HandleInput();
    public abstract void Update();
    public abstract void Exit();
}