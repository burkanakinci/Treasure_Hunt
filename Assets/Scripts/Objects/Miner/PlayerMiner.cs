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
        m_PlayerMinerStates.Add(new DigPlayerMinerState(this));
        m_PlayerMinerStates.Add(new FreezePlayerMinerState(this));

        PlayerMinerStateMachine = new PlayerMinerStateMachine(m_PlayerMinerStates);

        GameManager.Instance.Entities.OnFreezeAllMiner += FreezeMiner;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;

        m_PlayerMinerRadar.Initialize();

        GameManager.Instance.Entities.ManagerMinerList(this, ListOperation.Adding);

        MinerName = "Player";
    }

    private void Update()
    {

        PlayerMinerStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        PlayerMinerStateMachine.PhysicalUpdate();
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
            if ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
            {
                PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.DigPlayerMinerState);
            }
            EnteredLevelRadar();

        }
        if (other.CompareTag(ObjectTags.BOOST))
        {
            other.GetComponent<IBoost>().AffectBoost(this);
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
        m_PlayerMinerRadar.SetRadar(RadarType.RadarLevel1, TriggerType.Exit);
        PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.RunPlayerMinerState, true);
    }
    public BaseMinerAnimation GetMinerAnimation(MinerAnimations _minerAnimation)
    {
        return m_MinerAnimations[(int)_minerAnimation];
    }

    protected override void FreezeMiner()
    {
        base.FreezeMiner();
        PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.FreezePlayerMinerState, true);
    }
    public override void DissolveMiner()
    {
        PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.RunPlayerMinerState, true);
        base.DissolveMiner();
    }

    public override void EliminatedMiner()
    {
        base.EliminatedMiner();
        if (GameManager.Instance.Entities.RemainingMiner <= 3)
        {
            GameManager.Instance.LevelCompleted();
        }
        else
        {
            GameManager.Instance.LevelFailed();
        }

    }
    #region Events 
    private void OnResetToMainMenu()
    {
        for (int _animationCount = m_MinerAnimations.Length - 1; _animationCount >= 0; _animationCount--)
        {
            m_MinerAnimations[_animationCount].CloseHole();
        }
        m_CurrentSpeed = MinerData.MinerDefaultSpeed;
        MinerCollectedTreasure = 0;
        PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.IdlePlayerMinerState, true);

        transform.position = Vector3.zero;
    }
    private void OnCountdownFinished()
    {
        PlayerMinerStateMachine.ChangeState((int)PlayerMinerStates.RunPlayerMinerState);
    }
    private void OnLevelCompleted()
    {
        KillAllCoroutine();
    }
    private void OnLevelFailed()
    {
        KillAllCoroutine();
    }
    private void OnDestroy()
    {
    }
    #endregion
}
