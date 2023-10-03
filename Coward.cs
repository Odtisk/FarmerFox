using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Coward : MonoBehaviour
{
    [SerializeField] private float _speed;

    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private bool _hasDanger = false;
    private bool _safeDirection;

    private void Awake()
    {
        _collider = TryGetBoxColliderTrigger();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _hasDanger = collision.TryGetComponent<Player>(out var player);
        
        if (_hasDanger)
        {
            _safeDirection = player.transform.position.x < _collider.transform.position.x;
            _hasDanger = true;
        }
    }

    private void Update()
    {
        if (_hasDanger)
        {
            int directionCoefficient = _safeDirection ? 1 : -1;
            _rigidbody.velocity = new(_speed * directionCoefficient, 0);
        }
    }

    private BoxCollider2D TryGetBoxColliderTrigger()
    {
        var colliders = GetComponents<BoxCollider2D>();

        foreach (var collider in colliders)
            if (collider.isTrigger)
                return collider;

        colliders[0].isTrigger = true;
        return colliders[0];
    }
}
