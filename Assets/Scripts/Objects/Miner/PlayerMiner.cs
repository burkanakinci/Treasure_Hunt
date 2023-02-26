using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiner : BaseMiner
{
    public PlayerMinerStateMachine PlayerMinerStateMachine;
    [SerializeField] private Radar m_PlayerMinerRadar;
    public override void Initialize()
    {
        base.Initialize();

        List<IMinerState> m_PlayerMinerStates = new List<IMinerState>();
        m_PlayerMinerStates.Add(new IdlePlayerMinerState(this));
        m_PlayerMinerStates.Add(new RunPlayerMinerState(this));

        PlayerMinerStateMachine = new PlayerMinerStateMachine(m_PlayerMinerStates);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ObjectTags.RADAR))
        {
            LastTriggedTreasureRadar = other.GetComponent<TreasureRadar>();
            m_PlayerMinerRadar.SetRadar(LastTriggedTreasureRadar.TreasureRadarType, TriggerType.Enter);

            EnteredLevelRadar();

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(ObjectTags.RADAR))
        {
            LastTriggedTreasureRadar = other.GetComponent<TreasureRadar>();
            m_PlayerMinerRadar.SetRadar(LastTriggedTreasureRadar.TreasureRadarType, TriggerType.Exit);
                ExitLevelRadar();
        }
    }
    protected override void TreasureHunt()
    {
        base.TreasureHunt();
        m_PlayerMinerRadar.SetRadar(RadarType.RadarLevel1, TriggerType.Exit);
    }

    protected override void OnResetActiveTreasure()
    {
        base.OnResetActiveTreasure();
        m_PlayerMinerRadar.SetRadar(RadarType.RadarLevel1, TriggerType.Exit);
    }
    #region Events 
    private void OnResetToMainMenu()
    {
        m_MinerCollectedTreasure = 0;
        PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.IdlePlayerMinerState, true);
    }
    private void OnCountdownFinished()
    {
        PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.RunPlayerMinerState);
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
