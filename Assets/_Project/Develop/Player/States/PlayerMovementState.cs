using UnityEngine;

public class PlayerMovementState : PlayerState
{
    public PlayerMovementState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Player.Animator.Landing();
        Player.Effects.EnableMovement();
    }

    public override void Exit()
    {
        Player.Effects.DisableMovement();
    }

    public override void FixedUpdate()
    {
        Player.Movement.Move(Time.fixedDeltaTime);
    }
}
