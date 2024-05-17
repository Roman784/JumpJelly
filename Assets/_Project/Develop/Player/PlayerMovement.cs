using UnityEngine;

public class PlayerMovement
{
    private float _speed;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    public PlayerMovement(float speed, Transform transform, Rigidbody2D rigidbody)
    {
        _speed = speed;
        _transform = transform;
        _rigidbody = rigidbody;
    }

    public void Move(float delta)
    {
        _rigidbody.velocity = new Vector2(_transform.right.x * _speed * delta, _rigidbody.velocity.y);
    }

    public void TurnAround()
    {
        Vector3 angles = _transform.rotation.eulerAngles;

        angles.y = angles.y == 0f ? 180f : 0f;

        _transform.rotation = Quaternion.Euler(angles);
    }
}
