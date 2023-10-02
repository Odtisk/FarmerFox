using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private UnityEvent _taken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var _))
        {
            _taken?.Invoke();
            Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
