using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private float _distanceToWall;
    [SerializeField] private float _distanceToGround;

    [Space]

    [SerializeField] private LayerMask _surfaceLayerMask;

    public event Action<RaycastHit2D> OnWallTouched;
    public event Action<RaycastHit2D> OnGroundTouched;

    private void Update()
    {
        CheckWallTouch();
    }

    public bool IsWallTouch => CheckWallTouch();
    public bool IsInAir => CheckWallTouch() == false && OnGound() == false;

    public bool OnGound()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, _distanceToGround, _surfaceLayerMask);

        if (hit)
        {
            OnGroundTouched?.Invoke(hit);
            return true;
        }

        return false;
    }

    private bool CheckWallTouch()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _distanceToWall, _surfaceLayerMask);

        if (hit)
        {
            OnWallTouched?.Invoke(hit);
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, transform.right * _distanceToWall);
        Gizmos.DrawRay(transform.position, -transform.up * _distanceToGround);
    }
}
