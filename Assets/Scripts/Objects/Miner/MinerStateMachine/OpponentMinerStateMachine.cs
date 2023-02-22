using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMinerStateMachine : BaseStateMachine
{
    public OpponentMinerStateMachine(List<IMinerState> _states) : base(_states)
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
    }

    #region Events 
    private void OnResetToMainMenu()
    {
        ChangeState((int)OpponentMinerStates.IdleOpponentMinerState, true);
    }
    private void OnCountdownFinished()
    {
        ChangeState((int)OpponentMinerStates.RunOpponentMinerState, true);
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished -= OnCountdownFinished;
    }
    #endregion
}
