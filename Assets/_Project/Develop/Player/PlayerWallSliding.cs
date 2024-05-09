using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWallSliding : MonoBehaviour
{
    [SerializeField] private PlayerCollisionHandler _collisionHandler;

    [SerializeField] private float _gravity;
    private float _initialGravity;
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
        _initialGravity = _rigidbody.gravityScale;
    }

    private void Slide()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = _gravity;
    }

    public void Breake()
    {
        _rigidbody.gravityScale = _initialGravity;
    }

    private void OnWallTouched(RaycastHit2D hit)
    {
        if (_collisionHandler.OnGound() == false)
            Slide();
    }

    private void OnGroundTouched(RaycastHit2D hit)
    {
        Breake();
    }
}
