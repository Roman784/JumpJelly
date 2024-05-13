using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jumpEffect;
    [SerializeField] private ParticleSystem _wallSliding;
    [SerializeField] private GameObject _landingPrefab;
    [SerializeField] private GameObject _wallBumpPrefab;

    public void CreateLandingEffect(Vector2 position, Vector2 normal)
    {
        CreateEffect(_landingPrefab, position, normal);
    }

    public void CreateWallBumpEffect(Vector2 position, Vector2 normal)
    {
        CreateEffect(_wallBumpPrefab, position, normal);
    }

    public void PlayJumpEffect()
    {
        _jumpEffect.Play();
    }

    public void EnableWallSlidingEffect()
    {
        _wallSliding.Play();
    }

    public void DisableWallSlidingEffect()
    {
        _wallSliding.Stop();
    }

    private void CreateEffect(GameObject effect, Vector2 position, Vector2 normal)
    {
        GameObject spawnedEffect = Instantiate(effect, position, Quaternion.identity);
        spawnedEffect.transform.forward = normal;
    }
}
