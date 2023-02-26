using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMiner : CustomBehaviour
{
    public MinerData MinerData;
    #region Fields
    [SerializeField] protected Rigidbody2D m_PlayerMinerRB;
    [SerializeField] protected Animator m_MinerAnimator;
    protected int m_MinerCollectedTreasure;
    protected TreasureRadar m_TempTriggedTreasureRadar;
    protected Coroutine m_TreasureHuntCoroutine;
    #endregion

    public void SetMinerAnimatorValues(float _horizontalValue, float _verticalValue)
    {
        if ((_horizontalValue == 0.0f) && (_verticalValue == 0.0f))
        {
            return;
        }
        m_MinerAnimator.SetFloat("Horizontal", _horizontalValue);
        m_MinerAnimator.SetFloat("Vertical", _verticalValue);
    }

    public void SetMinerVelocity(Vector2 _targetVelocity)
    {
        m_PlayerMinerRB.velocity = _targetVelocity * MinerData.MinerDefaultSpeed;
    }

    public void SetMinerAnimatorSpeedValue(float _speed)
    {
        m_MinerAnimator.SetFloat("Speed", _speed);
    }

    protected void EnteredLevelRadar()
    {
        m_TempTriggedTreasureRadar.RadarTreasureGenerator.OnResetTreasure += OnResetActiveTreasure;
        if ((m_TempTriggedTreasureRadar.CanHunt == true) && (m_TempTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
        {
            m_TempTriggedTreasureRadar.CanHunt = false;
            StartTreasureHuntCoroutine();
        }
    }

    protected void ExitLevelRadar()
    {
        m_TempTriggedTreasureRadar.RadarTreasureGenerator.OnResetTreasure -= OnResetActiveTreasure;
        if ((m_TempTriggedTreasureRadar.CanHunt == true) && (m_TempTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
        {
            m_TempTriggedTreasureRadar.CanHunt = true;
            StopCoroutine(m_TreasureHuntCoroutine);
        }
    }

    protected void StartTreasureHuntCoroutine()
    {
        if (m_TreasureHuntCoroutine != null)
        {
            StopCoroutine(m_TreasureHuntCoroutine);
        }
        m_TreasureHuntCoroutine = StartCoroutine(TreasureHuntCoroutine());
    }

    protected IEnumerator TreasureHuntCoroutine()
    {
        yield return new WaitForSeconds(MinerData.TreasureDuration);
        TreasureHunt();
    }

    protected virtual void TreasureHunt()
    {
        m_MinerCollectedTreasure++;
        m_TempTriggedTreasureRadar.RadarTreasureGenerator.ResetTreasure();
    }

    protected virtual void OnResetActiveTreasure()
    {
    }
}
