using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    [SerializeField] private Eye[] _eyes;

    private Vector2 _previousPosition;

    private void Update()
    {
        RotateEyes();
    }

    private void RotateEyes()
    {
        if ((Vector2)transform.position == _previousPosition) return;

        Vector2 direction = ((Vector2)transform.position - _previousPosition).normalized;

        float offset = 90f;
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;

        foreach (var eye in _eyes)
        {
            RotatePupil(eye, angle);
        }
        
        _previousPosition = transform.position;
    }

    private void RotatePupil(Eye eye, float angle)
    {
        float x = Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = Mathf.Cos(angle * Mathf.Deg2Rad);

        Vector2 position = new Vector2(x, y) * eye.PupilDistance + (Vector2)eye.Sclera.position;

        eye.Pupil.position = position;
    }
}
