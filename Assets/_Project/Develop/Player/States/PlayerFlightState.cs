using UnityEngine;

public class PlayerFlightState : PlayerState
{
    public PlayerFlightState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Player.Animator.Flight(true);
        Player.Effects.EnableFlight();
    }

    public override void Exit()
    {
        Player.Animator.Flight(false);
        Player.Effects.DisableFlight();
    }

    public override void FixedUpdate()
    {
        Player.Movement.Move(Time.fixedDeltaTime);
    }
}
