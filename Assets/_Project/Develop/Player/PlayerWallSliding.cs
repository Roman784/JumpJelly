using UnityEngine;

public class PlayerWallSliding
{
    private float _speed;

    private Rigidbody2D _rigidbody;

    public PlayerWallSliding(float speed, Rigidbody2D rigidbody)
    {
        _speed = speed;
        _rigidbody = rigidbody;

        IsSliding = false;
    }

    public bool IsSliding { get; private set; }

    public void Slide(float delta)
    {
        // Limit the velocity of the fall.
        float y = Mathf.Clamp(_rigidbody.velocity.y, -_speed * delta, float.MaxValue); 
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, y);
    }

    public void StartSliding()
    {
        _rigidbody.velocity = Vector2.zero;
        IsSliding = true;
    }

    public void StopSliding()
    {
        IsSliding = false;
    }
}
