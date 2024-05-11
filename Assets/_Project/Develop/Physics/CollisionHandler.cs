using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action OnWallTouched;
    public event Action OnWallExited;
    public event Action OnGroundTouched;
    public event Action OnGroundExited;

    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _groundCheckDistance;

    [Space]

    [SerializeField] private LayerMask _surfaceLayer;

    // First touch since the last exit.
    private bool _firstWallTouch;
    private bool _firstGroundTouch;

    private void Awake()
    {
        _firstWallTouch = true;
        _firstGroundTouch = true;
    }

    private void Update()
    {
        CheckWall();
        CheckGround();
    }

    public bool IsTouchingWall => GetSurfaceHit(transform.right, _wallCheckDistance);
    public bool OnGround => GetSurfaceHit(-transform.up, _groundCheckDistance);
    public bool IsInAir => !IsTouchingWall && !OnGround;

    private void CheckWall()
    {
        CheckSurface(IsTouchingWall, ref _firstWallTouch, OnWallTouched, OnWallExited);
    }

    private void CheckGround()
    {
        CheckSurface(OnGround, ref _firstGroundTouch, OnGroundTouched, OnGroundExited);
    }
    
    private void CheckSurface(bool isTouching, ref bool firstTouch, Action onTouched, Action onExited)
    {
        if (isTouching)
        {
            if (!firstTouch) return;

            firstTouch = false;
            onTouched?.Invoke();
        }
        else
        {
            if (firstTouch) return;

            firstTouch = true;
            onExited?.Invoke();
        }
    }

    private RaycastHit2D GetSurfaceHit(Vector2 direction, float distance)
    {
        return Physics2D.Raycast(transform.position, direction, distance, _surfaceLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, transform.right * _wallCheckDistance);
        Gizmos.DrawRay(transform.position, -transform.up * _groundCheckDistance);
    }
}
