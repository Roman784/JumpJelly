using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSticking : MonoBehaviour
{
    [SerializeField] private PlayerCollisionHandler _collisionHandler;

    private Rigidbody2D _rigidbody;
    private float _initialGravity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _initialGravity = _rigidbody.gravityScale;
    }

    private void Start()
    {
        _collisionHandler.OnSurfaceEnter += Stick;
        _collisionHandler.OnSurfaceExit += EnableGravity;
    }

    private void OnDestroy()
    {
        _collisionHandler.OnSurfaceEnter -= Stick;
        _collisionHandler.OnSurfaceExit -= EnableGravity;
    }

    private void Stick(Collision2D collision, Surface surface)
    {
        DisableGravity();

        _rigidbody.velocity = Vector2.zero;
        Vector3 normal = collision.contacts[0].normal;
        transform.up = normal;
    }

    private void EnableGravity()
    {
        _rigidbody.gravityScale = _initialGravity;
    }

    private void DisableGravity()
    {
        _rigidbody.gravityScale = 0f;
    }

    private void EnableGravity(Collision2D collision, Surface surface)
    {
        EnableGravity();
    }
}
