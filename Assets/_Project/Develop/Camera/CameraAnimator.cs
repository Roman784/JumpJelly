using System.Collections;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public static CameraAnimator Instance;

    [SerializeField] private AnimationCurve _weakZoomCurve;

    [Space]

    [SerializeField] private Camera _camera;
    private float _initialSize;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _initialSize = _camera.orthographicSize;
    }

    public void WeakZoom()
    {
        StopAllCoroutines();
        StartCoroutine(PlayResizingRoutine(_weakZoomCurve));
    }

    private IEnumerator PlayResizingRoutine(AnimationCurve curve)
    {
        _camera.orthographicSize = _initialSize;

        for (float time = 0; time < curve.length; time += Time.deltaTime)
        {
            _camera.orthographicSize = _initialSize + curve.Evaluate(time);

            yield return null;
        }

        _camera.orthographicSize = _initialSize;
    }
}
