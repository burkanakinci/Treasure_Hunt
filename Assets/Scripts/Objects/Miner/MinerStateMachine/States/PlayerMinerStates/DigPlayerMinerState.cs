using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigPlayerMinerState : IMinerState
{
    private PlayerMiner m_Miner;
    public DigPlayerMinerState(PlayerMiner _miner)
    {
        m_Miner = _miner;
    }

    public void Enter()
    {
        m_Miner.SetMinerAnimatorTriggers(MinerAnimationParameters.DIG_TRIGGER);
        m_Miner.MinerHole.OpenHole(m_Miner.transform.position);
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
        m_Miner.MinerHole.CloseHole();
        m_Miner.SetMinerAnimatorTriggers(MinerAnimationParameters.IDLE_TRIGGER);
        m_Miner.SetMinerAnimatorValues(0.0f, 0.0f);
        m_Miner.SetMinerAnimatorSpeedValue(0.0f);
    }
}
