using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooledObjects : PooledObject
{
    [SerializeField] private ParticleSystem m_PlayedParticle;
    private Coroutine m_StartParticleIsAliveCoroutine;
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        StartParticleIsAliveCoroutine();
    }
    private void StartParticleIsAliveCoroutine()
    {
        if (m_StartParticleIsAliveCoroutine != null)
        {
            StopCoroutine(m_StartParticleIsAliveCoroutine);
        }

        m_StartParticleIsAliveCoroutine = StartCoroutine(ParticleIsAlive());
    }
    private IEnumerator ParticleIsAlive()
    {
        yield return new WaitUntil(() => (!m_PlayedParticle.IsAlive()));
        OnObjectDeactive();
    }
}
