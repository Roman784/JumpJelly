using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLauncher : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _abortDistance;

    public ReactiveProperty<bool> IsStretch = new ReactiveProperty<bool>();

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private Rigidbody2D _rigidbody;
    private Camera _camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        IsStretch.Value = false;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) StartPulling();
        if (Input.GetKey(KeyCode.Mouse0)) Stretch();
        if (Input.GetKeyUp(KeyCode.Mouse0)) Launch();
    }

    public Vector2 Direction => (_startPosition - _endPosition).normalized;
    public float StretchDistance => Vector2.Distance(_startPosition, _endPosition);
    public Vector2 StartPosition => _startPosition;

    private void StartPulling()
    {
        _startPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Stretch()
    {
        _endPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        IsStretch.Value = StretchDistance > _abortDistance;
    }

    private void Launch()
    {
        if (IsStretch.Value)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.velocity = Direction * StretchDistance * _force;
        }

        ResetStretch();
    }

    private void ResetStretch()
    {
        _startPosition = Vector2.zero;
        _endPosition = Vector2.zero;

        IsStretch.Value = false;
    }
}
