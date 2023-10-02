using System.Linq;
using UnityEngine;

public class Folower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _viewDistance;

    private Rigidbody2D _rigidbody;
    private float _spawnPositionX;
    private float _targetPositionX;
    private float _minDistanceForMove = 1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spawnPositionX = transform.position.x;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            _targetPositionX = player.transform.position.x;
        }
        else
        {
            _targetPositionX = _spawnPositionX;
        }
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FixedUpdate()
    {
        RaycastHit2D[] targets = GetTargets(false);
        targets.Concat(GetTargets(true));

        foreach (var target in targets)
        {
            if (target && target.collider.TryGetComponent<Player>(out var _))
            {
                _targetPositionX = target.transform.position.x;
                return;
            }
        }

        _targetPositionX = _spawnPositionX;
    }

    private RaycastHit2D[] GetTargets(bool isRightSide)
    {
        RaycastHit2D[] results = new RaycastHit2D[2];
        int directionCoefficient = isRightSide ? 1 : -1;
        _rigidbody.Cast(transform.right * directionCoefficient, results, _viewDistance);
        return results;
    }

    private void FollowTarget()
    {
        float distance = _targetPositionX - transform.position.x;

        if (Mathf.Abs(distance) > _minDistanceForMove)
        {
            bool isRirectedRight = distance > 0;
            int directionCoefficient = isRirectedRight ? 1 : -1;
            _rigidbody.velocity = new(_speed * directionCoefficient, 0);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
