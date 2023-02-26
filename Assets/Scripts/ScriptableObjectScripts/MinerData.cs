using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "MinerData", menuName = "Miner Data")]
public class MinerData : ScriptableObject
{

    #region Datas
    [SerializeField] private float m_MinerDefaultSpeed;
    [SerializeField] private float m_MinerFastSpeed;
    [SerializeField] private float m_TreasureDuration;

    [SerializeField] private LayerMask m_ObstacleLayers;
    [SerializeField] private float m_ObstacleRayDistance;

    #endregion

    #region ExternalAccess
    public float MinerDefaultSpeed => m_MinerDefaultSpeed;
    public float MinerFastSpeed => m_MinerFastSpeed;
    public float TreasureDuration => m_TreasureDuration;

    public LayerMask ObstacleLayers => m_ObstacleLayers;
    public float ObstacleRayDistance => m_ObstacleRayDistance;
    #endregion
}
