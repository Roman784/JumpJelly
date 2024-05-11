using UnityEngine;

[RequireComponent(typeof(Player), typeof(Rigidbody2D))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private float _force;

    private int _jumpCount; // For the double jump.

    private PlayerMovement _movement;
    private PlayerWallSliding _wallSliding;
    private CollisionHandler _collisionHandler;
    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        _collisionHandler.OnWallTouched += RestoreJumpCount;
        _collisionHandler.OnGroundTouched += RestoreJumpCount;
    }

    private void OnDisable()
    {
        _collisionHandler.OnWallTouched -= RestoreJumpCount;
        _collisionHandler.OnGroundTouched -= RestoreJumpCount;
    }

    private void Awake()
    {
        _movement = GetComponent<Player>().Movement;
        _wallSliding = GetComponent<Player>().WallSliding;
        _collisionHandler = GetComponent<Player>().CollisionHandler;
        _rigidbody = GetComponent<Rigidbody2D>();

        RestoreJumpCount();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        if (_collisionHandler.IsInAir && _jumpCount <= 0) return;

        if (_wallSliding.IsSliding)
            _movement.TurnAround();

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(transform.up * _force, ForceMode2D.Impulse);

        _jumpCount -= 1;
    }

    private void RestoreJumpCount()
    {
        _jumpCount = 2;
    }
}
