using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMiner : BaseMiner, IPooledObject
{
    #region PooledFields
    private Transform m_DeactiveParent;
    private string m_PooledTag;
    #endregion
    public OpponentDifficulty OpponentCurrentDifficulty;
    private OpponentMinerStateMachine m_OpponentStateMachine;
    public override void Initialize()
    {
        List<IMinerState> m_OpponentMinerStates = new List<IMinerState>();
        m_OpponentMinerStates.Add(new IdleOpponentMinerState(this));
        m_OpponentMinerStates.Add(new RunOpponentMinerState(this));

        m_OpponentStateMachine = new OpponentMinerStateMachine(m_OpponentMinerStates);
    }
    public void SetPooledTag(string _pooledTag)
    {
        m_PooledTag = _pooledTag;
    }
    public virtual void OnObjectSpawn()
    {
        m_OpponentStateMachine.ChangeState((int)OpponentMinerStates.IdleOpponentMinerState, true);
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
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

    private void Update()
    {
        m_OpponentStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        m_OpponentStateMachine.PhysicalUpdate();
    }

    #region Events 
    private void OnResetToMainMenu()
    {
    }
    private void OnCountdownFinished()
    {
        m_OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState, true);
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
