using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : CustomBehaviour
{
    [SerializeField] private Animator m_HoleAnimator;
    public override void Initialize()
    {
        CloseHole();
    }

    public void OpenHole(Vector2 _minerPos)
    {
        m_HoleAnimator.transform.position = _minerPos + Vector2.down;
        m_HoleAnimator.gameObject.SetActive(true);
        m_HoleAnimator.Play(AnimationStates.HOLE, 0, 0.0f);
    }
    public void CloseHole()
    {
        m_HoleAnimator.gameObject.SetActive(false);
    }
}
