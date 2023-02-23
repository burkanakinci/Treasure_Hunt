using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{

    #region Datas
    public List<Vector2> GroundPositions;
    public List<Vector2> SingleStonePositions;
    public List<Vector2> HorizontalStonePositions;
    public List<Vector2> VerticalStonePositions;
    public List<Vector2> RedTreePositions;
    public List<Vector2> GreenTreePositions;
    public List<Vector2> YellowTreePositions;
    public List<Vector2> OrangeTreePositions;
    public List<Vector2> TreasureGeneratorPositions;
    public List<float> TreasureGeneratorRefreshRates;
    public List<float> TreasureGeneratorDelays;
    public List<Vector2> TreasureGeneratorRefreshPositions;
    #endregion
}
