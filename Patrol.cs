using UnityEngine;
using DG.Tweening;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform _beginPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.position = _beginPoint.position;
        transform.DOMoveX(_endPoint.position.x, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Flash);
    }
}
