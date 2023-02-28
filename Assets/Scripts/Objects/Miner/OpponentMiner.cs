using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMiner : BaseMiner, IPooledObject
{
    #region Fields
    [SerializeField] private BoostTrigger m_BoostTrigger;
    #endregion
    #region PooledFields
    private Transform m_DeactiveParent;
    private string m_PooledTag;
    #endregion
    #region StatesFields
    [SerializeField] private Transform m_RayTransform;
    #endregion
    public OpponentDifficulty OpponentCurrentDifficulty;
    public OpponentMinerStateMachine OpponentStateMachine;
    public override void Initialize()
    {
        base.Initialize();

        List<IMinerState> m_OpponentMinerStates = new List<IMinerState>();
        m_OpponentMinerStates.Add(new IdleOpponentMinerState(this));
        m_OpponentMinerStates.Add(new RunOpponentMinerState(this));
        m_OpponentMinerStates.Add(new SearchTreasureOpponentState(this));
        m_OpponentMinerStates.Add(new DigOpponentMinerState(this));
        m_OpponentMinerStates.Add(new ReturnOpponentMinerState(this));
        m_OpponentMinerStates.Add(new FreezeOpponentMinerState(this));

        OpponentStateMachine = new OpponentMinerStateMachine(m_OpponentMinerStates);
    }
    #region PooledFunctions
    public void SetPooledTag(string _pooledTag)
    {
        m_PooledTag = _pooledTag;
    }
    public virtual void OnObjectSpawn()
    {
        for (int _animationCount = m_MinerAnimations.Length - 1; _animationCount >= 0; _animationCount--)
        {
            m_MinerAnimations[_animationCount].CloseHole();
        }
        m_CurrentSpeed = MinerData.MinerDefaultSpeed;

        OpponentStateMachine.ChangeState((int)OpponentMinerStates.IdleOpponentMinerState, true);

        GameManager.Instance.Entities.ManagerMinerList(this,ListOperation.Adding);

        SetMinerName();

        GameManager.Instance.Entities.OnFreezeAllMiner += FreezeMiner;
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
    }
    public virtual void OnObjectDeactive()
    {
        GameManager.Instance.Entities.ManagerMinerList(this,ListOperation.Subtraction);

        GameManager.Instance.Entities.OnFreezeAllMiner -= FreezeMiner;

        GameManager.Instance.OnCountdownFinished -= OnCountdownFinished;
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;

        GameManager.Instance.ObjectPool.AddObjectPool(m_PooledTag, this);

        this.transform.SetParent(m_DeactiveParent);
        this.gameObject.SetActive(false);
    }

    public CustomBehaviour GetGameObject()
    {
        return this;
    }
    #endregion
    private void Update()
    {
        OpponentStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        OpponentStateMachine.PhysicalUpdate();
    }
    public void SetOpponentAnimatorByTarget(Vector2 _targetPos, ref Vector2 _targetValue)
    {
        _targetValue = (_targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;

        SetMinerAnimatorValues(_targetValue.x, _targetValue.y);
        SetMinerAnimatorSpeedValue(_targetValue.sqrMagnitude);
    }

    #region CheckForwardArea
    public void SetRayTransform(Vector2 _targetPos)
    {
        m_RayTransform.position = transform.position;
        m_RayTransform.LookAt(_targetPos);
    }
    public bool CheckForward(Vector2 _targetPos)
    {

        if ((Physics2D.Raycast(
                 (m_RayTransform.position + m_RayTransform.up * 0.5f),
                 (_targetPos - new Vector2(transform.position.x, transform.position.y)),
                 (MinerData.ObstacleRayDistance),
                 (MinerData.ObstacleLayers))) ||
            (Physics2D.Raycast(
                 (m_RayTransform.position - m_RayTransform.up * 0.5f),
                 (_targetPos - new Vector2(transform.position.x, transform.position.y)),
                 (MinerData.ObstacleRayDistance),
                 (MinerData.ObstacleLayers)))
             )
        {

#if UNITY_EDITOR
            DrawForwardRay(Color.white, _targetPos);
#endif
            return false;
        }
        else
        {
#if UNITY_EDITOR
            DrawForwardRay(Color.black, _targetPos);
#endif
            return true;
        }
    }
#if UNITY_EDITOR

    private void DrawForwardRay(Color _drawRayColor, Vector2 _targetPos)
    {
        Debug.DrawRay
        (
            (m_RayTransform.position + m_RayTransform.up * 0.5f),
            ((_targetPos - new Vector2(transform.position.x, transform.position.y)).normalized * MinerData.ObstacleRayDistance),
            (_drawRayColor)
        );
        Debug.DrawRay
        (
            (m_RayTransform.position - m_RayTransform.up * 0.5f),
            ((_targetPos - new Vector2(transform.position.x, transform.position.y)).normalized * MinerData.ObstacleRayDistance),
            (_drawRayColor)
        );
    }
#endif
    #endregion
    [HideInInspector] public Vector2 LastEnteredRadarPos;
    [HideInInspector] public int LastTestedDirection;
    [HideInInspector] public int CurrentRadarLevel = -1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ObjectTags.RADAR))
        {
            LastTriggedTreasureRadar = other.GetComponent<TreasureRadar>();

            if ((CurrentRadarLevel == -1) || (CurrentRadarLevel < (int)LastTriggedTreasureRadar.TreasureRadarType))
            {
                LastTestedDirection = (-1);
                CurrentRadarLevel = (int)LastTriggedTreasureRadar.TreasureRadarType;
            }
            else
            {
                return;
            }
            LastEnteredRadarPos = (transform.position) + ((LastTriggedTreasureRadar.transform.position - transform.position).normalized * 1.15f);
            if ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
            {
                OpponentStateMachine.ChangeState((int)OpponentMinerStates.DigOpponentMinerState, true);
                EnteredLevelRadar();
            }
            else if (((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel1)) ||
            ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel2)))
            {
                OpponentStateMachine.ChangeState((int)OpponentMinerStates.SearchTreasureOpponentMinerState, true);
            }
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
            if ((int)LastTriggedTreasureRadar.TreasureRadarType == CurrentRadarLevel)
            {
                OpponentStateMachine.ChangeState((int)OpponentMinerStates.ReturnOpponentMinerState, true);
            }
        }
    }
    protected override void TreasureHunt()
    {
        base.TreasureHunt();
        OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState);

    }
    protected override void OnResetActiveTreasure()
    {
        OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState);
    }

    public BaseMinerAnimation GetMinerAnimation(MinerAnimations _minerAnimation)
    {
        return m_MinerAnimations[(int)_minerAnimation];
    }

    protected override void FreezeMiner()
    {
        base.FreezeMiner();
        OpponentStateMachine.ChangeState((int)OpponentMinerStates.FreezeOpponentMinerState, true);
    }

    public override void DissolveMiner()
    {
        OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState, true);
        base.DissolveMiner();
    }

    public override void EliminatedMiner()
    {
        base.EliminatedMiner();

        OnObjectDeactive();
    }
    #region Events 
    private void OnResetToMainMenu()
    {
    }
    private void OnCountdownFinished()
    {
        OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState);
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
