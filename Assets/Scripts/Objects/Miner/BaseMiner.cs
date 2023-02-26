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
    public TreasureRadar LastTriggedTreasureRadar;
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
        LastTriggedTreasureRadar.RadarTreasureGenerator.OnResetTreasure += OnResetActiveTreasure;
        if ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
        {
            LastTriggedTreasureRadar.CanHunt = false;
            StartTreasureHuntCoroutine();
        }
    }

    protected void ExitLevelRadar()
    {
        LastTriggedTreasureRadar.RadarTreasureGenerator.OnResetTreasure -= OnResetActiveTreasure;
        if ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
        {
            LastTriggedTreasureRadar.CanHunt = true;
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
        LastTriggedTreasureRadar.RadarTreasureGenerator.ResetTreasure();
    }

    protected virtual void OnResetActiveTreasure()
    {
    }
}
