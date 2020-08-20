using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : MovementState
{
    public PlayerState PreviousState { get; private set; }

    public StandingState(Player player, CharacterController characterController, Rigidbody rigidbody, Transform transform, PlayerState prevState) : base(player, characterController,
        rigidbody, transform)
    {
        MyPlayer = player;
        PreviousState = prevState;
        Name = "StandingState";
    }

    public override PlayerState HandleInput()
    {
        if (!IsJumping && MyPlayer.MyActivityState.Name == "IdleState")
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    if (MyPlayer.Stamina > 25)
                        return new SprintingState(MyPlayer, MyCharController, MyRigidBody, MyTransform, this);
                    else if (MyPlayer.Stamina > 0)
                        return new RunningState(MyPlayer, MyCharController, MyRigidBody, MyTransform);

                if (PreviousState == null)
                    return new WalkingState(MyPlayer, MyCharController, MyRigidBody, MyTransform);
                else
                    return PreviousState;
            }
        }

        return base.HandleInput();
    }

    public override void Update()
    {
        base.Update();
        if (MyPlayer.Hunger < 100)
        {
            MyPlayer.RegenStamina(Time.deltaTime * 3);
            MyPlayer.RegenHunger(Time.deltaTime);
        }
    }
}
