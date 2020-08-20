using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeState : EquipmentState
{
    public PickaxeState(Player player) : base(player)
    {
        Name = "PickaxeState";
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
                return new BowState(MyPlayer);

            // Scroll Wheel Backwards
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                return new AxeState(MyPlayer);

            if (MyPlayer.MyMovementState.Name != "SprintingState")
            {
                // Left Click
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject hitObject = GetObjectHit();
                    if (hitObject.transform.parent != null)
                        hitObject = hitObject.transform.parent.gameObject;
                    float distance = Math.Abs(Vector3.Distance(hitObject.transform.position, MyPlayer.gameObject.transform.position));
                    if (distance - hitObject.transform.localScale.x / 2 < 5)
                    {
                        ResourceHandler handler = hitObject.GetComponent<ResourceHandler>();
                        if (handler.IsMinable)
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
