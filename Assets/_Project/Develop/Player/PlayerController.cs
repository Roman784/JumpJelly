using System;
using UnityEngine;

public class PlayerController
{
    public event Action OnTouched;

    private bool _isEnabled;

    public PlayerController()
    {
        _isEnabled = true;
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    public void CheckTouch()
    {
        if (!_isEnabled) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            OnTouched?.Invoke();
        }
    }
}
