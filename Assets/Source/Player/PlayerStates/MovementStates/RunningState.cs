using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MovementState
{
    public RunningState(Player player, CharacterController characterController, Rigidbody rigidbody, Transform transform) : base(player, characterController, rigidbody, transform)
    {
        Speed = 12f;
        Name = "RunningState";
    }

    public override PlayerState HandleInput()
    {
        if (!IsJumping && MyPlayer.MyActivityState.Name == "IdleState")
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                return new StandingState(MyPlayer, MyCharController, MyRigidBody, MyTransform, this);

            if (Input.GetKeyUp(KeyCode.Slash) || MyPlayer.Stamina <= 5)
                return new WalkingState(MyPlayer, MyCharController, MyRigidBody, MyTransform);

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                if (MyPlayer.Stamina >= 15)
                    return new SprintingState(MyPlayer, MyCharController, MyRigidBody, MyTransform, this);

            if (MyPlayer.Stamina < 5)
                return new WalkingState(MyPlayer, MyCharController, MyRigidBody, MyTransform);
        }

        return base.HandleInput();
    }
    public override void Update()
    {
        base.Update();
        if (MyPlayer.Stamina >= 5)
            MyPlayer.DrainStamina(Time.deltaTime * 2);
    }
}
