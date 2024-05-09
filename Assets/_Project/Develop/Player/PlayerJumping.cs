using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private float _force;

    [Space]

    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerWallSliding _wallSliding;
    [SerializeField] private PlayerCollisionHandler _collisionHandler;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_collisionHandler.IsInAir) return;

        if (_collisionHandler.IsWallTouch)
        {
            _movement.TurnAround();
            _wallSliding.Breake();
        }

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(transform.up * _force, ForceMode2D.Impulse);
    }
}
