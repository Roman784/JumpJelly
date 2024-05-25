using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
[RequireComponent(typeof(PlayerEffects), typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;

    [Header("Wall sliding")]
    [SerializeField] private float _wallSlidingSpeed;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce;

    private PlayerStateHandler _stateHandler;
    private PlayerJumping _jumping;
    private CollisionHandler _collisionHandler;

    public PlayerMovement Movement { get; private set; }
    public PlayerWallSliding WallSliding { get; private set; }
    public PlayerAnimator Animator { get; private set; }
    public PlayerEffects Effects { get; private set; }

    private void OnEnable()
    {
        _collisionHandler.OnAnySurfaceTouched += _stateHandler.DetermineState;
        _collisionHandler.OnAnySurfaceExited += _stateHandler.DetermineState;

        _collisionHandler.OnWallTouched += OnWallTouched;
        _collisionHandler.OnGroundTouched += OnGroundTouched;

        _collisionHandler.OnDeadlyObjectTouched += Destroy;
    }

    private void OnDisable()
    {
        _collisionHandler.OnAnySurfaceTouched -= _stateHandler.DetermineState;
        _collisionHandler.OnAnySurfaceExited -= _stateHandler.DetermineState;

        _collisionHandler.OnWallTouched -= OnWallTouched;
        _collisionHandler.OnGroundTouched -= OnGroundTouched;

        _collisionHandler.OnDeadlyObjectTouched -= Destroy;
    }

    private void Awake()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Animator animator = GetComponent<Animator>();

        Movement = new PlayerMovement(_moveSpeed, transform, rigidbody);
        WallSliding = new PlayerWallSliding(_wallSlidingSpeed, rigidbody);
        Effects = GetComponent<PlayerEffects>();
        Animator = new PlayerAnimator(animator, Effects);

        _jumping = new PlayerJumping(_jumpForce, rigidbody);
        _collisionHandler = GetComponent<CollisionHandler>();

        _stateHandler = new PlayerStateHandler(this, _collisionHandler);
    }

    private void Update()
    {
        _stateHandler?.Update();

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        _stateHandler?.FixedUpdate();
    }

    private void Jump()
    {
        // Turns around if it slides down the wall.
        if (WallSliding.IsSliding)
            Movement.TurnAround();

        bool res = _jumping.Jump();
        if (res) Animator.Jump();
    }

    private void Destroy()
    {
        _stateHandler.Disable();

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        Animator.Destroy();
        Effects.Destroy();
    }

    private void OnWallTouched()
    {
        if (_collisionHandler.OnGround)
            Movement.TurnAround();

        _jumping.RestoreJumpCount();
    }

    private void OnGroundTouched()
    {
        if (_collisionHandler.IsTouchingWall)
            Movement.TurnAround();

        _jumping.RestoreJumpCount();
    }
}
