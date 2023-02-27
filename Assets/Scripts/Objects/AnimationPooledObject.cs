using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPooledObject : PooledObject
{
    [SerializeField] private Animator m_PooledAnimator;
    private Coroutine m_StartAnimationIsAliveCoroutine;

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        StartAnimationIsAliveCoroutine();
    }
    private void StartAnimationIsAliveCoroutine()
    {
        if (m_StartAnimationIsAliveCoroutine != null)
        {
            StopCoroutine(m_StartAnimationIsAliveCoroutine);
        }

        m_StartAnimationIsAliveCoroutine = StartCoroutine(AnimationIsAlive());
    }
    private IEnumerator AnimationIsAlive()
    {
        yield return new WaitUntil(() => (m_PooledAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f));
        OnObjectDeactive();
    }
}
