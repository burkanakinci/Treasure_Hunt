using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleOpponentMinerState : IMinerState
{
    private OpponentMiner m_Miner;
    public IdleOpponentMinerState(OpponentMiner _miner)
    {
        m_Miner = _miner;
    }

    public void Enter()
    {
        m_Miner.SetMinerAnimatorValues(0.0f,0.0f);
        m_Miner.SetMinerAnimatorSpeedValue(0.0f);
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

    }
}
