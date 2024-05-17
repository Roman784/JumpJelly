using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jump;
    [SerializeField] private ParticleSystem _movement;
    [SerializeField] private ParticleSystem _flight;
    [SerializeField] private ParticleSystem _wallSliding;
    [SerializeField] private ParticleSystem _landing;
    [SerializeField] private ParticleSystem _wallBump;

    public void Landing()
    {
        _landing.Play();
    }

    public void WallBump()
    {
        _wallBump.Play();
    }

    public void Jump()
    {
        _jump.Play();
    }

    public void EnableMovement()
    {
        _movement.Play();
    }

    public void DisableMovement()
    {
        _movement.Stop();
    }

    public void EnableFlight()
    {
        _flight.Play();
    }

    public void DisableFlight()
    {
        _flight.Stop();
    }

    public void EnableWallSliding()
    {
        _wallSliding.Play();
    }

    public void DisableWallSliding()
    {
        _wallSliding.Stop();
    }
}
