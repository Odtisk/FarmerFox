using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Animator))]
public class RunningAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _lastPositionX;
    private bool _isSpriteLeftOriented;
    private readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _isSpriteLeftOriented = _spriteRenderer.flipX;
        _lastPositionX = transform.position.x;
    }

    private void FixedUpdate()
    {
        bool hasMoved = _lastPositionX != transform.position.x;

        if (hasMoved)
        {
            bool isMovingLeft = _lastPositionX > transform.position.x;
            _spriteRenderer.flipX = _isSpriteLeftOriented != isMovingLeft;
            _lastPositionX = transform.position.x;
        }

        _animator.SetBool(IsRunning, hasMoved);
    }
}
