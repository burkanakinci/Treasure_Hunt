using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMinerAnimation : CustomBehaviour<string>
{
    [SerializeField] private Animator m_HoleAnimator;
    private string m_AnimationState;
    public override void Initialize(string _baseStateName)
    {
        m_AnimationState = _baseStateName;
        CloseHole();
    }

    public void OpenHole(Vector2 _minerPos)
    {
        m_HoleAnimator.transform.position = _minerPos + Vector2.down;
        m_HoleAnimator.gameObject.SetActive(true);
        m_HoleAnimator.Play(m_AnimationState, 0, 0.0f);
    }
    public void CloseHole()
    {
        m_HoleAnimator.gameObject.SetActive(false);
    }
}
