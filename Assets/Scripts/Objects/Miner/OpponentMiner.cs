using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMiner : BaseMiner
{
    private OpponentMinerStateMachine m_OpponentStateMachine;
    public override void Initialize()
    {
        base.Initialize();

        List<IMinerState> m_OpponentMinerStates = new List<IMinerState>();
        m_OpponentMinerStates.Add(new IdleOpponentMinerState(this));
        m_OpponentMinerStates.Add(new RunOpponentMinerState(this));

        m_OpponentStateMachine = new OpponentMinerStateMachine(m_OpponentMinerStates);
    }

    #region Events 
    private void OnResetToMainMenu()
    {
    }
    private void OnCountdownFinished()
    {
    }
    private void OnLevelCompleted()
    {
    }
    private void OnLevelFailed()
    {
    }
    private void OnDestroy()
    {
    }
    #endregion
}
