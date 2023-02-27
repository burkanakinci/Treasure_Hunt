using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBoost : PooledObject, IBoost
{
    public void AffectBoost(BaseMiner m_Miner)
    {
        m_Miner.FreezeOtherMiners();

        GameManager.Instance.ObjectPool.SpawnFromPool(
        (m_PooledTag),
        (GameManager.Instance.Entities.GetRandomPointOnGrounds()),
        (Quaternion.identity),
        (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Other))
        );
        OnObjectDeactive();
    }
}
