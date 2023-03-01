using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "CoinData", menuName = "Coin Data")]
public class CoinData : ScriptableObject
{

    #region Datas


    [Header("EarnedMovement")]
    [SerializeField] private float m_MinEarnedMovementDuration;
    [SerializeField] private float m_MaxEarnedMovementDuration;

    [Header("SpawnMovement")]
    [SerializeField] private float m_MinSpawmJumpDuration;
    [SerializeField] private float m_MaxSpawmJumpDuration;
    [SerializeField] private float m_SpawnJumpPower;
    [SerializeField] private float m_SpawnScaleTweenDuration;


    #endregion

    #region ExternalAccess
    public float EarnedMovementDuration => Random.Range(m_MinEarnedMovementDuration, m_MaxEarnedMovementDuration);
    public float SpawmJumpDuration => Random.Range(m_MinSpawmJumpDuration, m_MaxSpawmJumpDuration);
    public float SpawnJumpPower => m_SpawnJumpPower;
    public float SpawmScaleDuration => m_SpawnScaleTweenDuration;
    #endregion
}
