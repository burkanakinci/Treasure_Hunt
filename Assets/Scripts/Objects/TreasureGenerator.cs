using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureGenerator : PooledObject
{
    [SerializeField] private TreasureRadar[] m_TreasureRadars;
    public float GenerateRefreshRate;
    public float GenerateDelay;
    public override void Initialize()
    {
        base.Initialize();

        for (int _radarCount = m_TreasureRadars.Length - 1; _radarCount >= 0; _radarCount--)
        {
            m_TreasureRadars[_radarCount].Initialize();
        }
    }
    public override void OnObjectSpawn()
    {
        ResetTreasureAllRadar();
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }

    private void ResetTreasureAllRadar()
    {
        for (int _radarCount = m_TreasureRadars.Length - 1; _radarCount >= 0; _radarCount--)
        {
            m_TreasureRadars[_radarCount].CanHunt = true;
        }
    }
}
