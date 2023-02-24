using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureGenerator : PooledObject
{
    [SerializeField] private Transform m_RadarParents;
    [SerializeField] private TreasureRadar[] m_TreasureRadars;
    public float GenerateRefreshRate;
    private Coroutine m_RefreshTreasureCoroutine;
    private Vector2 m_RefreshPosition;
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
        m_RadarParents.gameObject.SetActive(true);
        ResetTreasureAllRadar();
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAlCoroutine();
        GameManager.Instance.Entities.ManageTreasureGeneratorList(this, ListOperation.Subtraction);
        base.OnObjectDeactive();
    }

    private void ResetTreasureAllRadar()
    {
        for (int _radarCount = m_TreasureRadars.Length - 1; _radarCount >= 0; _radarCount--)
        {
            m_TreasureRadars[_radarCount].CanHunt = true;
        }
    }

    public void ResetTreasureGenerator()
    {
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
        Debug.Log(GenerateRefreshRate);
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
