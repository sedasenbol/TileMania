using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaceAnchor : MonoBehaviour
{
    private const float ROTATION_ANGLE = 40f;
    private const float ROTATION_TIME = 2f;
    private void Rotate()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0f, 0f, -ROTATION_ANGLE), ROTATION_TIME));
        sequence.Append(transform.DORotate(new Vector3(0f, 0f, ROTATION_ANGLE), ROTATION_TIME));
        sequence.SetLoops(-1);
    }
    private void Update()
    {
    }
    private void Start()
    {
        Rotate();
    }
}
