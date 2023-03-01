using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "TreasureGeneratorData", menuName = "Treasure Generator Data")]
public class TreasureGeneratorData : ScriptableObject
{

    #region Datas
     [SerializeField] private int m_MinTreasureEarnCount;
    [SerializeField] private int m_MaxTreasureEarnCount;
    #endregion

    #region ExternalAccess
    public int TreasureEarnCount => Random.Range(m_MinTreasureEarnCount, m_MaxTreasureEarnCount);
    #endregion
}
