using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentState : PlayerState
{  
    public EquipmentState(Player player) : base(player)
    {
        Name = "EquipmentState";
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override PlayerState HandleInput()
    {
        return null;
    }

    public override void Update()
    {
        
    }

    protected GameObject GetObjectHit()
    {
        Ray ray = MyPlayer.MyCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }
}
