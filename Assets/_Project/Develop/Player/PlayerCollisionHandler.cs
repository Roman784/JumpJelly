using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public event Action<Collision2D, Surface> OnSurfaceEnter;
    public event Action<Collision2D, Surface> OnSurfaceExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Surface>(out Surface surface))
        {
            OnSurfaceEnter?.Invoke(collision, surface);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Surface>(out Surface surface))
        {
            OnSurfaceExit?.Invoke(collision, surface);
        }
    }
}
