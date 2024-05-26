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

    public PlayerController Controller { get; private set; }
    public PlayerPhysics Physics { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerWallSliding WallSliding { get; private set; }
    public PlayerAnimator Animator { get; private set; }
    public PlayerEffects Effects { get; private set; }

    private void OnEnable()
    {
        Controller.OnTouched += Jump;

        _collisionHandler.OnAnySurfaceTouched += _stateHandler.DetermineState;
        _collisionHandler.OnAnySurfaceExited += _stateHandler.DetermineState;
        _collisionHandler.OnDeadlyObjectTouched += _stateHandler.SetDestroyState;

        _collisionHandler.OnWallTouched += OnWallTouched;
        _collisionHandler.OnGroundTouched += OnGroundTouched;
    }

    private void OnDisable()
    {
        Controller.OnTouched -= Jump;

        _collisionHandler.OnAnySurfaceTouched -= _stateHandler.DetermineState;
        _collisionHandler.OnAnySurfaceExited -= _stateHandler.DetermineState;
        _collisionHandler.OnDeadlyObjectTouched -= _stateHandler.SetDestroyState;

        _collisionHandler.OnWallTouched -= OnWallTouched;
        _collisionHandler.OnGroundTouched -= OnGroundTouched;
    }

    private void Awake()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Animator animator = GetComponent<Animator>();

        Controller = new PlayerController();
        Physics = new PlayerPhysics(rigidbody);
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
        Controller.CheckTouch();
        _stateHandler?.Update();
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
