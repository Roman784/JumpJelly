using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;

    [Space]

    [SerializeField] private TrailRenderer _trailRenderer;
    private float _initialTrailTime;

    [Space]

    [SerializeField] private PlayerEffects _effects;

    private Animator _animator;

    private void OnEnable()
    {
        _collisionHandler.OnWallTouched += OnWallTouched;
        _collisionHandler.OnGroundTouched += OnGroundTouched;
        _collisionHandler.OnWallExited += OnWallExited;
        _collisionHandler.OnGroundExited += OnGroundExited;
    }

    private void OnDisable()
    {
        _collisionHandler.OnWallTouched -= OnWallTouched;
        _collisionHandler.OnGroundTouched -= OnGroundTouched;
        _collisionHandler.OnWallExited -= OnWallExited;
        _collisionHandler.OnGroundExited -= OnGroundExited;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _initialTrailTime = _trailRenderer.time;

        DisableTrail();
    }

    private void Landing()
    {
        _animator.SetTrigger("Landing");
        DisableTrail();

        RaycastHit2D hit = _collisionHandler.GetGroundHit();
        _effects.CreateLandingEffect(hit.point, hit.normal);

        _effects.EnableMovementEffect();
        _effects.DisableWallSlidingEffect();
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        EnableTrail();

        _effects.DisableMovementEffect();
        _effects.DisableWallSlidingEffect();
    }

    private void WallBump()
    {
        _animator.SetTrigger("WallBump");
        DisableTrail();

        RaycastHit2D hit = _collisionHandler.GetWallHit();
        _effects.CreateWallBumpEffect(hit.point, hit.normal);

        _effects.EnableWallSlidingEffect();
    }

    private void EnableTrail()
    {
        _trailRenderer.emitting = true;
    }

    private void DisableTrail()
    {
        _trailRenderer.emitting = false;
    }

    private void OnGroundTouched()
    {
        if (!_collisionHandler.IsTouchingWall)
        {
            Landing();
        }
    }

    private void OnWallTouched()
    {
        if (!_collisionHandler.OnGround)
        {
            WallBump();
        }
    }

    private void OnGroundExited()
    {
        if (!_collisionHandler.IsTouchingWall)
        {
            Jump();
        }
    }

    private void OnWallExited()
    {
        if (!_collisionHandler.OnGround)
        {
            Jump();
        }
    }
}
