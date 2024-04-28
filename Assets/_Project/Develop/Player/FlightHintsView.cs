using UnityEngine;

public class FlightHintsView : MonoBehaviour
{
    [SerializeField] private Transform _directionArrow;
    [SerializeField] private Transform _stretchArrow;

    [Space]

    [SerializeField] private float _stertchArrowLengthMultiplier;

    [Space]

    [SerializeField] private PlayerLauncher _launcher;

    private void Start()
    {
        _launcher.IsStretch.OnChanged += OnLaunchStretchChanged;

        DisableArrows();
    }

    private void OnDestroy()
    {
        _launcher.IsStretch.OnChanged -= OnLaunchStretchChanged;
    }

    private void OnLaunchStretchChanged(bool isStretch)
    {
        if (isStretch)
        {
            EnableArrows();

            RenderDirectionArrow();
            RenderStretchArrow();
        }
        else
        {
            DisableArrows();
        }
    }

    private void RenderDirectionArrow()
    {
        _directionArrow.up = _launcher.Direction;
    }

    private void RenderStretchArrow()
    {
        _stretchArrow.up = _launcher.Direction;
        _stretchArrow.position = _launcher.StartPosition;
        _stretchArrow.localScale = new Vector2(_stretchArrow.localScale.x, _launcher.StretchDistance * _stertchArrowLengthMultiplier);
    }

    private void EnableArrows()
    {
        SetActiveArrows(true);
    }

    private void DisableArrows()
    {
        SetActiveArrows(false);
    }

    private void SetActiveArrows(bool value)
    {
        _directionArrow.gameObject.SetActive(value);
        _stretchArrow.gameObject.SetActive(value);
    }
}
