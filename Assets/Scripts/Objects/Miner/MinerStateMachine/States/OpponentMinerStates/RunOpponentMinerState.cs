using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOpponentMinerState : IMinerState
{
    private OpponentMiner m_Miner;
    public RunOpponentMinerState(OpponentMiner _miner)
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
