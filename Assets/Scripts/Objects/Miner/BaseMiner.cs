using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class BaseMiner : CustomBehaviour
{

    #region Fields
    [HideInInspector] public string MinerName;
    [SerializeField] protected TextMeshPro m_NameText;
    [SerializeField] private SpriteRenderer m_MinerRenderer;
    protected float m_CurrentSpeed;
    public MinerData MinerData;
    [SerializeField] protected Rigidbody2D m_PlayerMinerRB;
    [SerializeField] protected Animator m_MinerAnimator;
    [SerializeField] private ParticleSystem m_FreezeParticle;
    public int MinerCollectedTreasure;
    [HideInInspector] public TreasureRadar LastTriggedTreasureRadar;
    protected Coroutine m_TreasureHuntCoroutine;

    #region Events
    public event Action EarnedCoin;
    #endregion
    [SerializeField] protected BaseMinerAnimation[] m_MinerAnimations;
    #endregion

    public override void Initialize()
    {

        m_MinerAnimations[0].Initialize(AnimationStates.HOLE);
        m_OnResetTreasureDelayedCallID = GetInstanceID() + "m_OnResetTreasureDelayedCallID";

    }
    protected virtual void SetMinerName()
    {
        MinerName = MinerData.OpponentName;
        m_NameText.text = MinerName;
    }
    public void SetMinerVelocity(Vector2 _targetVelocity)
    {
        m_PlayerMinerRB.velocity = _targetVelocity * m_CurrentSpeed;
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
            LastTriggedTreasureRadar.CachedComponent.SpawnCoin(this);
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
        LastTriggedTreasureRadar.CachedComponent.ResetTreasure();
    }

    private Coroutine m_SpeedDefaultCoroutine;
    public void SpeedUpMiner()
    {
        m_CurrentSpeed = MinerData.MinerFastSpeed;
        StartSpeedDefaultCoroutine();
    }
    private void StartSpeedDefaultCoroutine()
    {
        if (m_SpeedDefaultCoroutine != null)
        {
            StopCoroutine(m_SpeedDefaultCoroutine);
        }
        m_SpeedDefaultCoroutine = StartCoroutine(SpeedDefaultCoroutine());
    }

    private IEnumerator SpeedDefaultCoroutine()
    {
        yield return new WaitForSeconds(MinerData.SpeedUpDuration);
        m_CurrentSpeed = MinerData.MinerDefaultSpeed;
    }

    private string m_OnResetTreasureDelayedCallID;
    protected virtual void OnResetActiveTreasure()
    {
        DOTween.Kill(m_OnResetTreasureDelayedCallID);

        DOVirtual.DelayedCall((0.8f), () =>
        {
            EarnedCoin?.Invoke();
        }).
        SetId(m_OnResetTreasureDelayedCallID);
    }

    protected virtual void KillAllCoroutine()
    {
        if (m_SpeedDefaultCoroutine != null)
        {
            StopCoroutine(m_SpeedDefaultCoroutine);
        }
        if (m_TreasureHuntCoroutine != null)
        {
            StopCoroutine(m_TreasureHuntCoroutine);
        }
    }

    protected virtual void FreezeMiner()
    {
        m_MinerRenderer.color = MinerData.FreezeColor;
        m_MinerAnimator.enabled = false;
        m_FreezeParticle.Play();
        KillAllCoroutine();
    }

    public virtual void FreezeOtherMiners()
    {
        GameManager.Instance.Entities.OnFreezeAllMiner -= FreezeMiner;
        GameManager.Instance.Entities.FreezeAllMiner();

        StartAddedFreezeCoroutine();
    }
    public virtual void EliminatedMiner()
    {

    }
    private Coroutine m_AddedFreezeCoroutine;
    private void StartAddedFreezeCoroutine()
    {
        if (m_AddedFreezeCoroutine != null)
        {
            StopCoroutine(m_AddedFreezeCoroutine);
        }

        m_AddedFreezeCoroutine = StartCoroutine(AddedFreezeCoroutine());
    }

    private IEnumerator AddedFreezeCoroutine()
    {
        yield return new WaitForSeconds(MinerData.FreezeDuration - 0.1f);
        GameManager.Instance.Entities.OnFreezeAllMiner += FreezeMiner;
    }

    public virtual void DissolveMiner()
    {
        m_MinerRenderer.color = MinerData.DefaultColor;
        m_MinerAnimator.enabled = true;
        m_FreezeParticle.Stop();
    }


}
