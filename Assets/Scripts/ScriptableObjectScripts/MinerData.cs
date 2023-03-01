using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "MinerData", menuName = "Miner Data")]
public class MinerData : ScriptableObject
{

    #region Datas
    [SerializeField] private float m_MinerDefaultSpeed;
    [SerializeField] private float m_MinerFastSpeed;
    [SerializeField] private float m_SpeedUpDuration;
    [SerializeField] private float m_TreasureDuration;
    [SerializeField] private float m_FreezeDuration;
    [SerializeField] private LayerMask m_ObstacleLayers;
    [SerializeField] private float m_ObstacleRayDistance;
    [SerializeField] private string[] m_OpponentMinerNames;
    [SerializeField] private Color m_FreezeColor;
    [SerializeField] private Color m_DefaultColor;

    #endregion

    #region ExternalAccess
    public float MinerDefaultSpeed => m_MinerDefaultSpeed;
    public float MinerFastSpeed => m_MinerFastSpeed;
    public float TreasureDuration => m_TreasureDuration;
    public float SpeedUpDuration => m_SpeedUpDuration;
    public LayerMask ObstacleLayers => m_ObstacleLayers;
    public float ObstacleRayDistance => m_ObstacleRayDistance;
    public float FreezeDuration => m_FreezeDuration;
    public Color FreezeColor => m_FreezeColor;
    public Color DefaultColor => m_DefaultColor;
    public string OpponentName => m_OpponentMinerNames[Random.Range(0, m_OpponentMinerNames.Length - 1)];

    #endregion
}
