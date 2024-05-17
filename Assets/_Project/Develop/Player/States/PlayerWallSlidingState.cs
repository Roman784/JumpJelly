using UnityEngine;

public class PlayerWallSlidingState : PlayerState
{
    public PlayerWallSlidingState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Player.Animator.WallBump();
        Player.Effects.EnableWallSliding();
        Player.WallSliding.StartSliding();
    }

    public override void Exit()
    {
        Player.Effects.DisableWallSliding();
        Player.WallSliding.StopSliding();
    }

    public override void FixedUpdate()
    {
        Player.WallSliding.Slide(Time.fixedDeltaTime);
    }
}
