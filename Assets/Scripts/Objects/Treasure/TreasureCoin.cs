using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreasureCoin : PooledObject
{
    [SerializeField] private Transform m_CoinVisual;
    [SerializeField] private CoinData m_CoinData;
    private BaseMiner m_EarnedMiner;

    public override void Initialize()
    {
        base.Initialize();
        m_CoinEarnedTweenID = GetInstanceID() + "m_CoinEarnedTweenID";
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        m_CoinVisual.localScale = Vector3.zero;

    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
        KillAllTween();
        m_CoinVisual.localScale = Vector3.zero;
        m_EarnedMiner.EarnedCoin -= CoinEarnedMovement;
    }

    public void SetEarnedMiner(BaseMiner _miner)
    {
        m_EarnedMiner = _miner;
        _miner.EarnedCoin += CoinEarnedMovement;
    }

    private string m_CoinSpawnMovementTweenID;
    private string m_CoinSpawnScaleTweenID;
    public void CoinSpawnJumpTween()
    {
        DOTween.Kill(m_CoinSpawnMovementTweenID);

        transform.DOLocalJump(
            (m_EarnedMiner.transform.position + (new Vector3(Random.Range(-1.25f, 2.25f), Random.Range(-1.75f, 0.75f), 0.0f))),
            (m_CoinData.SpawnJumpPower),
            (1),
            (m_CoinData.SpawmJumpDuration),
            (false)
        ).
        SetId(m_CoinSpawnMovementTweenID);
    }
    public void CoinSpawnScaleTween()
    {
        DOTween.Kill(m_CoinSpawnScaleTweenID);

        m_CoinVisual.DOScale(
            (Vector3.one),
            (m_CoinData.SpawmScaleDuration)
        ).
        SetId(m_CoinSpawnScaleTweenID);
    }

    private string m_CoinEarnedTweenID;
    private void CoinEarnedMovement()
    {
        DOTween.Kill(m_CoinEarnedTweenID);

        transform.DOMove(
            (m_EarnedMiner.transform.position),
            (m_CoinData.EarnedMovementDuration)
        ).
        OnUpdate(() =>
        {
            if (!m_EarnedMiner.enabled)
            {
                DOTween.Kill(m_CoinEarnedTweenID);
                OnObjectDeactive();
            }
        }).
        OnComplete(() =>
        {
            m_EarnedMiner.MinerCollectedTreasure++;
            GameManager.Instance.Entities.OrderMinerCollectedTreasure();
            m_EarnedMiner.EarnedCoin -= CoinEarnedMovement;
            OnObjectDeactive();
        }).
        SetId(m_CoinEarnedTweenID);
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_CoinEarnedTweenID);
    }
}
