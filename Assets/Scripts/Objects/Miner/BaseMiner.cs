using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMiner : CustomBehaviour
{

    #region Fields
    public MinerData MinerData;
    [SerializeField] protected Rigidbody2D m_PlayerMinerRB;
    [SerializeField] protected Animator m_MinerAnimator;
    protected int m_MinerCollectedTreasure;
    [HideInInspector] public TreasureRadar LastTriggedTreasureRadar;
    protected Coroutine m_TreasureHuntCoroutine;
    [SerializeField] protected BaseMinerAnimation[] m_MinerAnimations;
    #endregion

    public override void Initialize()
    {

        m_MinerAnimations[0].Initialize(AnimationStates.HOLE);

    }
    public void SetMinerVelocity(Vector2 _targetVelocity)
    {
        m_PlayerMinerRB.velocity = _targetVelocity * MinerData.MinerDefaultSpeed;
    }

    public void SetMinerAnimatorValues(float _horizontalValue, float _verticalValue)
    {
        if ((_horizontalValue == 0.0f) && (_verticalValue == 0.0f))
        {
            return;
        }
        SetMinerAnimatorParameter(MinerAnimationParameters.HORIZONTAL, _horizontalValue);
        SetMinerAnimatorParameter(MinerAnimationParameters.VERTICAL, _verticalValue);
    }

    public void SetMinerAnimatorSpeedValue(float _speed)
    {
        SetMinerAnimatorParameter(MinerAnimationParameters.SPEED, _speed);
    }

    public void SetMinerAnimatorParameter(string _parameter, float _parameterValue)
    {
        m_MinerAnimator.SetFloat(_parameter.ToString(), _parameterValue);
    }
    public void SetMinerAnimatorTriggers(string _parameter)
    {
        m_MinerAnimator.SetTrigger(_parameter);
    }

    protected void EnteredLevelRadar()
    {
        LastTriggedTreasureRadar.CachedComponent.OnResetTreasure += OnResetActiveTreasure;
        if ((LastTriggedTreasureRadar.CanHunt == true) && (LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel3))
        {
            LastTriggedTreasureRadar.CanHunt = false;
            StartTreasureHuntCoroutine();
        }
    }

    protected void ExitLevelRadar()
    {
        LastTriggedTreasureRadar.CachedComponent.OnResetTreasure -= OnResetActiveTreasure;
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
        LastTriggedTreasureRadar.CachedComponent.ResetTreasure();
    }

    protected virtual void OnResetActiveTreasure()
    {
    }
}
