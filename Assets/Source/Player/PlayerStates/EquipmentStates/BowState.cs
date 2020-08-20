using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowState : EquipmentState
{
    public BowState(Player player) : base(player)
    {
        Name = "BowState";
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
                return new UnarmedState(MyPlayer);

            // Scroll Wheel Backwards
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                return new PickaxeState(MyPlayer);

            if (MyPlayer.MyMovementState.Name != "SprintingState")
            {
                // Left Click
                if (Input.GetMouseButtonDown(0))
                {
                    if (MyPlayer.GetAmmunitionCapacity() > 0)
                    {
                        GameObject hitObject = GetObjectHit();
                        if (hitObject != null)
                        {
                            float distance = Math.Abs(Vector3.Distance(hitObject.transform.position, MyPlayer.gameObject.transform.position));
                            if (distance < 100)
                            {
                                ResourceHandler handler = hitObject.GetComponent<ResourceHandler>();
                                if (handler.IsHuntable)
                                {
                                    AIController aIController = hitObject.GetComponent<AIController>();
                                    aIController.GetShot();
                                }
                            }
                        }
                        MyPlayer.ShootArrow();
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
