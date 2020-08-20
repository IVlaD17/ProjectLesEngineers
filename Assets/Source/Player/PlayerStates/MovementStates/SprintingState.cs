using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintingState : MovementState
{
    public PlayerState PreviousState { get; private set; }

    public SprintingState(Player player, CharacterController characterController, Rigidbody rigidbody, Transform transform, PlayerState prevState) : base(player, characterController, rigidbody, transform)
    {
        Speed = 18f;
        PreviousState = prevState;
        Name = "SprintingState";
    }

    public override PlayerState HandleInput()
    {
        if (!IsJumping && MyPlayer.MyActivityState.Name == "IdleState")
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                return new StandingState(MyPlayer, MyCharController, MyRigidBody, MyTransform, this);

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                return PreviousState;

            if (MyPlayer.Stamina < 15)
                return new RunningState(MyPlayer, MyCharController, MyRigidBody, MyTransform);

            if (MyPlayer.Stamina < 5)
                return new WalkingState(MyPlayer, MyCharController, MyRigidBody, MyTransform);
        }

        return base.HandleInput();
    }
    public override void Update()
    {
        base.Update();
        if (MyPlayer.Stamina >= 5)
            MyPlayer.DrainStamina(Time.deltaTime * 4);
    }
}
