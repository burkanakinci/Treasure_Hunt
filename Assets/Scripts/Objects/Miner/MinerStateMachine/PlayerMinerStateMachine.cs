using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinerStateMachine : BaseStateMachine
{
    public PlayerMinerStateMachine(List<IMinerState> _states) : base(_states)
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
    }

    #region Events 
    private void OnResetToMainMenu()
    {
        ChangeState((int)PlayerMinerStates.IdlePlayerMinerState, true);
    }
    private void OnCountdownFinished()
    {
        ChangeState((int)PlayerMinerStates.RunPlayerMinerState, true);
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished -= OnCountdownFinished;
    }
    #endregion
}
