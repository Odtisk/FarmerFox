using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _lastPositionY;
    private bool _isGrounded;
    private float _damageKickback = 10;
    private float _hitKickback = 7;

    private readonly int IsBeaten = Animator.StringToHash(nameof(IsBeaten));
    private readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));
    private readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _lastPositionY = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Enemy>(out var _))
        {
            collision.otherRigidbody.AddForce(Vector2.up * _damageKickback);
            _animator.SetTrigger(IsBeaten);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy) && collision.isTrigger == false)
        {            
            enemy.Die();
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _hitKickback);
        }

        _isGrounded = true;
    }

    private void OnTriggerExit2D()
    {
        _isGrounded = false;
    }

    private void FixedUpdate()
    {
        bool isMovingUp = _lastPositionY < transform.position.y;

        _animator.SetBool(IsJumping, _isGrounded == false && isMovingUp);
        _animator.SetBool(IsFalling, _isGrounded == false && isMovingUp == false);        

        _lastPositionY = transform.position.y;
    }
}
