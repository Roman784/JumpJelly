using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Space]

    [SerializeField] private CollisionHandler _collisionHandler;

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
    }

    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
    }

    private void Move(float delta)
    {
        _rigidbody.velocity = new Vector2(transform.right.x * _speed * delta, _rigidbody.velocity.y);
    }

    public void TurnAround()
    {
        Vector3 angles = transform.rotation.eulerAngles;

        angles.y = angles.y == 0f ? 180f : 0f;

        transform.rotation = Quaternion.Euler(angles);
    }

    private void OnWallTouched()
    {
        if (_collisionHandler.OnGround)
            TurnAround();
    }

    private void OnGroundTouched()
    {
        if (_collisionHandler.IsTouchingWall)
            TurnAround();
    }
}
