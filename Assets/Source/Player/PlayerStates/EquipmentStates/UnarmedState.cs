using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnarmedState : EquipmentState
{
    public UnarmedState(Player player) : base(player)
    {
        Name = "UnarmedState";
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
                return new AxeState(MyPlayer);

            // Scroll Wheel Backwards
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                return new BowState(MyPlayer);

            if (MyPlayer.MyMovementState.Name != "SprintingState")
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    GameObject hitObject = GetObjectHit();
                    if (hitObject.transform.parent != null)
                        hitObject = hitObject.transform.parent.gameObject;
                    float distance = Math.Abs(Vector3.Distance(hitObject.transform.position, MyPlayer.gameObject.transform.position));
                    if (hitObject.name.Contains("Loot"))
                    {
                        if(distance < 7.5f)
                        {
                            LootHandler handler = hitObject.GetComponent<LootHandler>();
                            if (MyPlayer.HasSpaceInInventory())
                                MyPlayer.GatherItem(handler.CollectLoot());
                        }
                    }
                    else
                    {
                        if (hitObject.name.Contains("Mushroom"))
                            distance -= 25f;
                        if (distance < 7.5f)
                        {
                            ResourceHandler handler = hitObject.GetComponent<ResourceHandler>();
                            if (handler.IsHarvestable)
                            {
                                if (MyPlayer.HasSpaceInInventory())
                                    MyPlayer.GatherItem(handler.Harvest());
                            }
                        }
                    }
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
