using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiner : BaseMiner
{
    private PlayerMinerStateMachine m_PlayerMinerStateMachine;
    [SerializeField] private Rigidbody2D m_PlayerMinerRB;
    [SerializeField] private Radar m_PlayerMinerRadar;
    public override void Initialize()
    {
        base.Initialize();

        List<IMinerState> m_PlayerMinerStates = new List<IMinerState>();
        m_PlayerMinerStates.Add(new IdlePlayerMinerState(this));
        m_PlayerMinerStates.Add(new RunPlayerMinerState(this));

        m_PlayerMinerStateMachine = new PlayerMinerStateMachine(m_PlayerMinerStates);

        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;

        m_PlayerMinerRadar.Initialize();
    }

    public void MoveMinerByJoystick(float _speed, float _horizontalValue, float _verticalValue)
    {
        SetMinerAnimatorSpeedValue(_speed);
        SetMinerVelocity(new Vector2(_horizontalValue, _verticalValue));
        SetMinerAnimatorValues(_horizontalValue, _verticalValue);
    }

    private void SetMinerVelocity(Vector2 _targetVelocity)
    {
        m_PlayerMinerRB.velocity = _targetVelocity * m_MinerData.MinerDefaultSpeed;
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