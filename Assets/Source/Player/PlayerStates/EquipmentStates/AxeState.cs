using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeState : EquipmentState
{
    public AxeState(Player player) : base(player)
    {
        Name = "AxeState";
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
        if (MyPlayer.MyActivityState.Name == "IdleState")
        {
            // Scroll Wheel Forward
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                return new PickaxeState(MyPlayer);

            // Scroll Wheel Backwards
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                return new UnarmedState(MyPlayer);

            if (MyPlayer.MyMovementState.Name != "SprintingState")
            {
                // Left Click
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject hitObject = GetObjectHit();
                    if (hitObject.transform.parent != null)
                        hitObject = hitObject.transform.parent.gameObject;
                    float distance = Math.Abs(Vector3.Distance(hitObject.transform.position, MyPlayer.gameObject.transform.position));
                    if (distance < 10)
                    {
                        ResourceHandler handler = hitObject.GetComponent<ResourceHandler>();
                        if (handler.IsHackable)
                        {
                            handler.Harvest();
                        }
                    }
                }

                // Right Click
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log(Name + " Right Click Pressed");
                }

                // Scroll Wheel Click
                if (Input.GetMouseButtonDown(2))
                {
                    Debug.Log(Name + " Scroll Wheel Pressed");
                }
            }
        }

        return base.HandleInput();
    }

    public override void Update()
    {
        base.Update();
    }
}
