using UnityEngine;

public class PlayerJumping
{
    private float _force;
    private int _jumpCount; // For the double jump.

    private Rigidbody2D _rigidbody;

    public PlayerJumping(float force, Rigidbody2D rigidbody)
    {
        _force = force;
        _rigidbody = rigidbody;

        RestoreJumpCount();
    }

    public bool Jump()
    {
        if (_jumpCount <= 0) return false;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);

        _jumpCount -= 1;

        return true;
    }

    public void RestoreJumpCount()
    {
        _jumpCount = 2;
    }
}
