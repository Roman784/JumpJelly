using UnityEngine;

[RequireComponent(typeof(Player), typeof(Rigidbody2D))]
public class PlayerWallSliding : MonoBehaviour
{
    [SerializeField] private float _speed;

    private CollisionHandler _collisionHandler;
    private Rigidbody2D _rigidbody;    

    private void OnEnable()
    {
        _collisionHandler.OnWallTouched += StartSliding;
        _collisionHandler.OnWallExited += StopSliding;
    }

    private void OnDisable()
    {
        _collisionHandler.OnWallTouched -= StartSliding;
        _collisionHandler.OnWallExited -= StopSliding;
    }

    private void Awake()
    {
        _collisionHandler = GetComponent<Player>().CollisionHandler;
        _rigidbody = GetComponent<Rigidbody2D>();

        IsSliding = false;
    }

    private void FixedUpdate()
    {
        if (IsSliding)
            Slide(Time.fixedDeltaTime);
    }

    public bool IsSliding { get; private set; }

    private void Slide(float delta)
    {
        // Limit the velocity of the fall.
        float y = Mathf.Clamp(_rigidbody.velocity.y, -_speed * delta, float.MaxValue); 
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, y);
    }

    private void StartSliding()
    {
        if (_collisionHandler.OnGround) return;

        _rigidbody.velocity = Vector2.zero;
        IsSliding = true;
    }

    public void StopSliding()
    {
        IsSliding = false;
    }
}
