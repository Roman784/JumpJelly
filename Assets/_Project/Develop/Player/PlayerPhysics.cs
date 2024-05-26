using UnityEngine;

public class PlayerPhysics
{
    private Rigidbody2D _rigidbody;

    public PlayerPhysics(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Freeze()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    public void Unfreeze()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
