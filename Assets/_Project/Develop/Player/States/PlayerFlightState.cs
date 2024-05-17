using UnityEngine;

public class PlayerFlightState : PlayerState
{
    public PlayerFlightState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Player.Animator.Flight();
        Player.Effects.EnableFlight();
    }

    public override void Exit()
    {
        Player.Effects.DisableFlight();
    }

    public override void FixedUpdate()
    {
        Player.Movement.Move(Time.fixedDeltaTime);
    }
}
