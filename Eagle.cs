using UnityEngine;
using DG.Tweening;

public class Eagle : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _duration;

    private readonly int IsDiving = Animator.StringToHash(nameof(IsDiving));

    private void Start()
    {
        transform.position = _points[0].position;
        var path = ConvertTransformToVector3(_points);
        transform.DOPath(path, _duration, PathType.CatmullRom).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBack);
    }

    private Vector3[] ConvertTransformToVector3(Transform[] points)
    {
        Vector3[] vectors = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            vectors[i] = points[i].position;
        }

        return vectors;
    }
}
