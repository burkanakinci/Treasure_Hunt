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
        m_OpponentMinerStates.Add(new RunOpponentMinerState(this,m_RayTransform));

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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ObjectTags.RADAR))
        {
            m_TempTriggedTreasureRadar = other.GetComponent<TreasureRadar>();
            if ((m_TempTriggedTreasureRadar.CanHunt == true) && (m_TempTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
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
            m_TempTriggedTreasureRadar = other.GetComponent<TreasureRadar>();
            if ((m_TempTriggedTreasureRadar.CanHunt == true) && (m_TempTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
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

    private void Update()
    {
        OpponentStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        OpponentStateMachine.PhysicalUpdate();
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
