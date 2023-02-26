using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMiner : BaseMiner, IPooledObject
{
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
        List<IMinerState> m_OpponentMinerStates = new List<IMinerState>();
        m_OpponentMinerStates.Add(new IdleOpponentMinerState(this));
        m_OpponentMinerStates.Add(new RunOpponentMinerState(this));
        m_OpponentMinerStates.Add(new SearchTreasureOpponentState(this));

        OpponentStateMachine = new OpponentMinerStateMachine(m_OpponentMinerStates);
    }
    public void SetPooledTag(string _pooledTag)
    {
        m_PooledTag = _pooledTag;
    }
    public virtual void OnObjectSpawn()
    {
        OpponentStateMachine.ChangeState((int)OpponentMinerStates.IdleOpponentMinerState, true);
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
    }
    public virtual void OnObjectDeactive()
    {
        GameManager.Instance.OnCountdownFinished -= OnCountdownFinished;
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        this.transform.SetParent(null);
        GameManager.Instance.ObjectPool.AddObjectPool(m_PooledTag, this);
        this.transform.SetParent(m_DeactiveParent);
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        OpponentStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        OpponentStateMachine.PhysicalUpdate();
    }
    public void MoveOpponentToTarget(Vector2 _targetPos, ref Vector2 _targetValue)
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

    public Vector2 TempEnteredRadarPos;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ObjectTags.RADAR))
        {
            LastTriggedTreasureRadar = other.GetComponent<TreasureRadar>();
            TempEnteredRadarPos=transform.position;

            if (((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel1)) ||
            ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel2)))
            {
                OpponentStateMachine.ChangeState((int)OpponentMinerStates.SearchTreasureOpponentState, true);
            }
            else if ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
            {
                OpponentStateMachine.ChangeState((int)OpponentMinerStates.IdleOpponentMinerState, true);
                EnteredLevelRadar();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(ObjectTags.RADAR))
        {
            LastTriggedTreasureRadar = other.GetComponent<TreasureRadar>();
            if (((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel1)) ||
            ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel2)))
            {
                OpponentStateMachine.ChangeState((int)OpponentMinerStates.SearchTreasureOpponentState, true);
            }
            if ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
            {
                ExitLevelRadar();
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
        base.OnResetActiveTreasure();
        OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState, true);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
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
