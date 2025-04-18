public class PlayerDestroyState : PlayerState
{
    public PlayerDestroyState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Player.Controller.Disable();
        Player.Physics.Freeze();
        Player.Animator.Destroy();

        CameraAnimator.Instance.WeakZoom();
    }

    public override void Exit()
    {
        Player.Controller.Enable();
        Player.Physics.Unfreeze();
    }
}