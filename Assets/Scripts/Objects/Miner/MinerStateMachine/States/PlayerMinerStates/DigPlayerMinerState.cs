using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DigPlayerMinerState : IMinerState
{
    private PlayerMiner m_Miner;
    public DigPlayerMinerState(PlayerMiner _miner)
    {
        m_Miner = _miner;
        m_DigVFXDelayedCallID = m_Miner.GetInstanceID() + "m_DigVFXDelayedCallID";
    }

    public void Enter()
    {
        m_Miner.SetMinerAnimatorTriggers(MinerAnimationParameters.DIG_TRIGGER);
        m_Miner.GetMinerAnimation(MinerAnimations.Hole).OpenAnimation(m_Miner.transform.position);
        m_OnDigging = true;
        StartDigVFXDelayedCall();
    }

    private string m_DigVFXDelayedCallID;
    private bool m_OnDigging;
    private void StartDigVFXDelayedCall()
    {
        DOTween.Kill(m_DigVFXDelayedCallID);

        DOVirtual.DelayedCall(
            (1.2f),
            (() =>
            {
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.DIG_DUST_PARTICLE),
                    (m_Miner.transform.position - (Vector3.up * 0.15f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Other))
                );
                if (m_OnDigging)
                {
                    StartDigVFXDelayedCall();
                }
            })
        ).
        SetId(m_DigVFXDelayedCallID);
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {
        m_Miner.SetMinerVelocity(Vector2.zero);
    }
    public void Exit()
    {
        DOTween.Kill(m_DigVFXDelayedCallID);
        m_OnDigging = false;

        GameManager.Instance.ObjectPool.SpawnFromPool(
            (PooledObjectTags.POUCH_ANIMATION),
            (m_Miner.transform.position),
            (Quaternion.identity),
            (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Other))
        );
        m_Miner.GetMinerAnimation(MinerAnimations.Hole).CloseAnimation();
        m_Miner.SetMinerAnimatorTriggers(MinerAnimationParameters.IDLE_TRIGGER);
        m_Miner.SetMinerAnimatorValues(0.0f, 0.0f);
        m_Miner.SetMinerAnimatorSpeedValue(0.0f);
    }
}
