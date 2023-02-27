using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PooledObject, IBoost
{
    public void AffectBoost(BaseMiner m_Miner)
    {
        m_Miner.SpeedUpMiner();
        
        GameManager.Instance.ObjectPool.SpawnFromPool(
        (m_PooledTag),
        (GameManager.Instance.Entities.GetRandomPointOnGrounds()),
        (Quaternion.identity),
        (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Other))
        );
        OnObjectDeactive();
    }
}
