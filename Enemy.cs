using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private UnityEvent _dead;

    private Animator _animator;
    private readonly int Dead = Animator.StringToHash(nameof(Dead));

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Die()
    {
        _dead?.Invoke();
        _animator.SetTrigger(Dead);
        Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
