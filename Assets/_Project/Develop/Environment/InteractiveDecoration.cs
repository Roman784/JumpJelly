using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractiveDecoration : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Shake()
    {
        _animator.SetTrigger("Shaking");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shake();
    }
}
