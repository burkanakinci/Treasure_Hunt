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
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {

    }
    public void Exit()
    {

    }
}
