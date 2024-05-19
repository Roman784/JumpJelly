using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _strength; // The farther the layer, the higher the value.
    
    private Transform _target;
    Vector2 _targetPreviousPosition;

    private void Start()
    {
        _target = Camera.main.transform;
        _targetPreviousPosition = _target.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if ((Vector2)_target.position == _targetPreviousPosition) return;

        Vector2 delta = (Vector2)_target.position - _targetPreviousPosition;

        transform.position += (Vector3)delta * _strength * Time.deltaTime;

        _targetPreviousPosition = _target.position;
    }
}
