using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class TreasureGenerator : PooledObject
{
    #region Fields
    [SerializeField] private TreasureGeneratorData m_TreasureGeneratorData;
    [SerializeField] private Transform m_RadarParents;
    [SerializeField] private TreasureRadar[] m_TreasureRadars;
    public float GenerateRefreshRate;
    private Coroutine m_RefreshTreasureCoroutine;
    private Vector2 m_RefreshPosition;
    #endregion
    #region Events
    public event Action OnResetTreasure;
    #endregion

    public override void Initialize()
    {
        base.Initialize();
        m_CoinSpawnDelayedCallID = GetInstanceID() + "m_CoinSpawnDelayedCallID";

        for (int _radarCount = m_TreasureRadars.Length - 1; _radarCount >= 0; _radarCount--)
        {
            m_TreasureRadars[_radarCount].Initialize(this);
        }
    }
    public override void OnObjectSpawn()
    {
        OnResetTreasure += OnResetTreasureGenerator;
        GameManager.Instance.Entities.ManageTreasureGeneratorList(this, ListOperation.Adding);
        m_RadarParents.gameObject.SetActive(true);
        ResetTreasureAllRadar();
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        OnResetTreasure -= OnResetTreasureGenerator;
        KillAlCoroutine();
        GameManager.Instance.Entities.ManageTreasureGeneratorList(this, ListOperation.Subtraction);
        m_RadarParents.gameObject.SetActive(false);
        base.OnObjectDeactive();
    }

    private void ResetTreasureAllRadar()
    {
        for (int _radarCount = m_TreasureRadars.Length - 1; _radarCount >= 0; _radarCount--)
        {
            m_TreasureRadars[_radarCount].CanHunt = true;
        }
    }

    public void ResetTreasure()
    {
        OnResetTreasure?.Invoke();
    }

    private void OnResetTreasureGenerator()
    {
        GameManager.Instance.Entities.ManageTreasureGeneratorList(this, ListOperation.Subtraction);
        m_RadarParents.gameObject.SetActive(false);
        StartRefreshTreasureCoroutine();
    }

    private void StartRefreshTreasureCoroutine()
    {
        if (m_RefreshTreasureCoroutine != null)
        {
            StopCoroutine(m_RefreshTreasureCoroutine);
        }
        m_RefreshTreasureCoroutine = StartCoroutine(RefreshTreasureCoroutine());
    }

    private IEnumerator RefreshTreasureCoroutine()
    {
        yield return new WaitForSeconds(GenerateRefreshRate);
        m_RefreshPosition = GameManager.Instance.Entities.SpawnNewTreasureGenerator();
        this.gameObject.transform.position = m_RefreshPosition;
        OnObjectSpawn();
    }

    private BaseMiner m_EarnedMiner;
    private void KillAlCoroutine()
    {
        if (m_RefreshTreasureCoroutine != null)
        {
            StopCoroutine(m_RefreshTreasureCoroutine);
        }
    }

    #region CoinArea
    private int m_TempCoinCount, m_CoinCounter;
    private float m_SpawnCoinValue;
    private string m_CoinSpawnDelayedCallID;
    public void SpawnCoin(BaseMiner _miner)
    {
        return;
        m_TempCoinCount = m_TreasureGeneratorData.TreasureEarnCount;
        m_EarnedMiner = _miner;
        m_CoinCounter = 0;
        m_SpawnCoinValue = 0.0f;

        DOTween.To(() => m_SpawnCoinValue, x => m_SpawnCoinValue = x, (2.0f), (2.0f)).
        OnUpdate(
            () =>
            {
                SpawnCoinCoroutine(UnityEngine.Random.Range(0.02f, (m_TreasureGeneratorData.TreasureEarnCount - m_SpawnCoinValue)) - 0.25f);
            }
            ).
        OnComplete(
            () =>
            {
                ResetTreasure();
            }
            ).
        SetId(m_CoinSpawnDelayedCallID);

    }

    private IEnumerator SpawnCoinCoroutine(float _spawnDelay)
    {
        yield return new WaitForSeconds(_spawnDelay);
        GameManager.Instance.ObjectPool.SpawnFromPool(
            (PooledObjectTags.TREASURE_COIN),
            (m_EarnedMiner.transform.position),
            (Quaternion.identity),
            (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Other))
        ).GetGameObject().GetComponent<TreasureCoin>().SetEarnedMiner(m_EarnedMiner);
    }


    #endregion
}
