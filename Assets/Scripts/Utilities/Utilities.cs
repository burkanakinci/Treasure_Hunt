
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class Utilities
{
    public static void Open(this CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public static void Close(this CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    public static Vector3 PointOnPour(ObjectsLayer _rayMaskLayer, Vector3 _toPos, Vector3 _fromPos, float _circleRadius)
    {
        int m_LayerMask = 1 << ((int)_rayMaskLayer);
        RaycastHit m_Hit;
        _toPos.x = _toPos.x + (UnityEngine.Random.insideUnitCircle * _circleRadius).x;
        _toPos.z = _toPos.z + (UnityEngine.Random.insideUnitCircle * _circleRadius).y;
        Vector3 m_RayDirection = _toPos - _fromPos;

        if (Physics.Raycast(_fromPos, m_RayDirection, out m_Hit, 2.0f, m_LayerMask))
        {
            return m_Hit.point;
        }
        else
        {
            return _toPos;
        }
    }
}
