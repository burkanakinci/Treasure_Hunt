using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigOpponentMinerState : IMinerState
{
    private OpponentMiner m_Miner;
    public DigOpponentMinerState(OpponentMiner _miner)
    {
        m_Miner = _miner;
    }

    public void Enter()
    {
        m_Miner.SetMinerAnimatorTriggers(MinerAnimationParameters.DIG_TRIGGER);
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
        m_Miner.SetMinerAnimatorTriggers(MinerAnimationParameters.IDLE_TRIGGER);
        m_Miner.SetMinerAnimatorValues(0.0f, 0.0f);
        m_Miner.SetMinerAnimatorSpeedValue(0.0f);
    }
}
