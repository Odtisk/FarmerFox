using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3.4f;
    [SerializeField] private float _jumpHeight = 6.5f;
    [SerializeField] private float _gravityModifier = 1.5f;
    [SerializeField] private UnityEvent _jumped;

    private float _moveDirection = 0;
    private bool _isGrounded = false;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _mainCollider;
    private BoxCollider2D _footCollider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCollider = GetComponent<CapsuleCollider2D>();
        _footCollider = GetComponent<BoxCollider2D>();
        _rigidbody.freezeRotation = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rigidbody.gravityScale = _gravityModifier;
    }

    private void Update()
    {
        _moveDirection = Input.GetAxis("Horizontal");        

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _jumped?.Invoke();
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpHeight);
        }
    }

    private void FixedUpdate()
    {
        Bounds colliderBounds = _mainCollider.bounds;
        float colliderRadius = _mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);

        _isGrounded = false;

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _mainCollider && colliders[i] != _footCollider)
                {
                    _isGrounded = true;
                    break;
                }
            }
        }

        _rigidbody.velocity = new Vector2(_moveDirection * _maxSpeed, _rigidbody.velocity.y);
    }
}