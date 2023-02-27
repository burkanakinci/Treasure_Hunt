using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{

    #region Datas
    public List<Vector2> GroundPositions;
    public List<Vector2> StonePositions;
    public List<StoneType> StoneTypes;
    public List<Vector2> TreePositions;
    public List<TreeType> TreeTypes;
    public List<Vector2> TreasureGeneratorPositions;
    public List<float> TreasureGeneratorRefreshRates;
    public List<OpponentDifficulty> Opponents;
    public int FreezeBoostCount;
    public int SpeedBoostCount;
    #endregion
}
