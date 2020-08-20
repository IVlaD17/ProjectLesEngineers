using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : PlayerState
{
    public CharacterController MyCharController { get; protected set; }
    public Transform MyTransform { get; protected set; }
    public Rigidbody MyRigidBody { get; protected set; }

    public float Speed { get; protected set; }
    public float Gravity { get; protected set; }

    public bool IsJumping { get; protected set; }
    public float JumpSpeed { get; protected set; }

    private Vector3 moveDirection = Vector3.zero;

    public MovementState(Player player, CharacterController characterController, Rigidbody rigidbody, Transform transform) : base(player)
    {
        MyCharController = characterController;
        MyRigidBody = rigidbody;
        MyTransform = transform;
        Gravity = 9.8f;
        JumpSpeed = 7.5f;
        IsJumping = false;
        Name = "MoveState";
    }

    public override void Enter()
    {
        
    }

    public override PlayerState HandleInput()
    {
        if (MyCharController != null)
        {
            if(MyCharController.isGrounded)
            {
                IsJumping = false;

                moveDirection = new Vector3(Input.GetAxis("Horizontal") * Speed, 0.0f, Input.GetAxis("Vertical") * Speed);
                moveDirection = MyTransform.TransformDirection(moveDirection);

                if(Input.GetButton("Jump") && MyPlayer.Stamina >= 15)
                {
                    MyPlayer.DrainStamina(5f);
                    moveDirection.y = JumpSpeed * MyPlayer.Stamina / 100;
                    IsJumping = true;
                }
            }

            moveDirection.y -= Gravity * Time.deltaTime;
            MyCharController.Move(moveDirection * Time.deltaTime);
        }

        return null;
    }

    public override void Update()
    {
        if (MyPlayer.Hunger == 100)
            MyPlayer.DrainHealth(Time.deltaTime);
    }

    public override void Exit()
    {
        
    }
}
