using UnityEngine;
using DG.Tweening;

public class VerticalSwing : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _distance;

    private void Start()
    {
        transform.DOMoveY(transform.position.y + _distance, _duration).SetLoops(-1, LoopType.Yoyo);
    }
}
