public abstract class PlayerState
{
    protected readonly Player Player;

    public PlayerState(Player player)
    {
        Player = player;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
