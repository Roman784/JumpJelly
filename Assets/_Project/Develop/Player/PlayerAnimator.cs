using UnityEngine;

public class PlayerAnimator
{
    private Animator _animator;
    private PlayerEffects _effects;

    public PlayerAnimator(Animator animator, PlayerEffects effects)
    {
        _animator = animator;
        _effects = effects;
    }

    public void Landing()
    {
        _animator.SetTrigger("Landing");
        _effects.Landing();
    }

    public void Jump()
    {
        _animator.SetTrigger("Jump");
        _effects.Jump();
    }

    public void WallBump()
    {
        _animator.SetTrigger("WallBump");
        _effects.WallBump();
    }

    public void Flight(bool enabled)
    {
        _animator.SetBool("Flight", enabled);
    }
}
