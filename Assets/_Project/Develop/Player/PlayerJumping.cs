using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private float _force;

    [SerializeField] private int _maxJumpCount;
    private int _jumpCounter;

    [Space]

    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerWallSliding _wallSliding;
    [SerializeField] private PlayerCollisionHandler _collisionHandler;

    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        _collisionHandler.OnWallTouched += OnWallTouched;
        _collisionHandler.OnGroundTouched += OnGroundTouched;
    }

    private void OnDisable()
    {
        _collisionHandler.OnWallTouched -= OnWallTouched;
        _collisionHandler.OnGroundTouched -= OnGroundTouched;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        RestoreJumpCount();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_collisionHandler.IsInAir && _jumpCounter <= 0) return;

        if (_collisionHandler.IsWallTouch)
        {
            _movement.TurnAround();
            _wallSliding.Breake();
        }

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(transform.up * _force, ForceMode2D.Impulse);

        _jumpCounter -= 1;
    }

    private void RestoreJumpCount()
    {
        _jumpCounter = _maxJumpCount;
    }

    private void OnWallTouched(RaycastHit2D hit)
    {
        RestoreJumpCount();
    }

    private void OnGroundTouched(RaycastHit2D hit)
    {
        RestoreJumpCount();
    }
}
