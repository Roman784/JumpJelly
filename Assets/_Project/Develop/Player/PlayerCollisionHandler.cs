using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private float _distanceToWall;
    [SerializeField] private float _distanceToGround;

    [Space]

    [SerializeField] private LayerMask _surfaceLayerMask;

    public event Action<RaycastHit2D> OnWallTouched;

    private void Update()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, transform.right, _distanceToWall, _surfaceLayerMask);

        if (wallHit)
        {
            OnWallTouched?.Invoke(wallHit);
        }
    }

    public bool OnGound()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, _distanceToGround, _surfaceLayerMask);

        return hit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, transform.right * _distanceToWall);
        Gizmos.DrawRay(transform.position, -transform.up * _distanceToGround);
    }
}
