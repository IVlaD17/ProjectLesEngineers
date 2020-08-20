using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : MovementState
{
    public WalkingState(Player player, CharacterController characterController, Rigidbody rigidbody, Transform transform) : base(player, characterController, rigidbody, transform)
    {
        Speed = 6f;
        Name = "WalkingState";
    }

    public override PlayerState HandleInput()
    {
        if (!IsJumping && MyPlayer.MyActivityState.Name == "IdleState")
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                return new StandingState(MyPlayer, MyCharController, MyRigidBody, MyTransform, this);

            if (Input.GetKeyUp(KeyCode.Slash))
                if (MyPlayer.Stamina > 0)
                    return new RunningState(MyPlayer, MyCharController, MyRigidBody, MyTransform);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                if (MyPlayer.Stamina > 25)
                    return new SprintingState(MyPlayer, MyCharController, MyRigidBody, MyTransform, this);
                else if (MyPlayer.Stamina > 0)
                    return new RunningState(MyPlayer, MyCharController, MyRigidBody, MyTransform);
        }

        return base.HandleInput();
    }
    public override void Update()
    {
        base.Update();
        if (MyPlayer.Hunger < 100)
            MyPlayer.RegenHunger(Time.deltaTime);

        if(MyPlayer.Hunger < 75)
            MyPlayer.RegenStamina(Time.deltaTime * 2);
    }
}
