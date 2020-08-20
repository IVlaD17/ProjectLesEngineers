using UnityEngine;
using System.Collections;

public class InventoryState : ActivityState
{
    public InventoryState(Player player) : base(player)
    {
        Name = "InventoryState";
    }

    public override void Enter()
    {
        GUI.Instance.ToggleControlPanel();
        base.Enter();
    }

    public override void Exit()
    {
        GUI.Instance.ToggleControlPanel();
        base.Exit();
    }

    public override PlayerState HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
            return new IdleState(MyPlayer);

        return base.HandleInput();
    }

    public override void Update()
    {
        base.Update();
    }
}
