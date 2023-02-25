using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreasureGenerator : PooledObject
{
    #region Fields

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

        for (int _radarCount = m_TreasureRadars.Length - 1; _radarCount >= 0; _radarCount--)
        {
            m_TreasureRadars[_radarCount].Initialize(this);
        }
    }
    public override void OnObjectSpawn()
    {
        OnResetTreasure+=OnResetTreasureGenerator;
        GameManager.Instance.Entities.ManageTreasureGeneratorList(this, ListOperation.Adding);
        m_RadarParents.gameObject.SetActive(true);
        ResetTreasureAllRadar();
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        OnResetTreasure-=OnResetTreasureGenerator;
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

    private void KillAlCoroutine()
    {
        if (m_RefreshTreasureCoroutine != null)
        {
            StopCoroutine(m_RefreshTreasureCoroutine);
        }
    }
}
