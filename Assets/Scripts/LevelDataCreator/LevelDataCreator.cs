
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class LevelDataCreator : MonoBehaviour
{
    [HideInInspector] public LevelData TempLevelData;
    private string m_SavePath;
    [HideInInspector] public int LevelNumber;
    #region ObjectsOnScene
    private List<GameObject> m_GroundsOnScene;
    private List<GameObject> m_SingleStonesOnScene;
    private List<GameObject> m_HorizontalStonesOnScene;
    private List<GameObject> m_VerticalStonesOnScene;
    private List<GameObject> m_RedTreesOnScene;
    private List<GameObject> m_GreenTreesOnScene;
    private List<GameObject> m_YellowTreesOnScene;
    private List<GameObject> m_OrangeTreesOnScene;
    private List<TreasureGenerator> m_TreasureGeneratorsOnScene;
    #endregion
    public void CreateLevel()
    {
        TempLevelData = ScriptableObject.CreateInstance<LevelData>();

        m_SavePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelDatas/" + LevelNumber + "LevelData.asset");

        SetGroundData();

        SetStoneData();

        SetTreeData();

        SetTreasureGeneratorData();

        AssetDatabase.CreateAsset(TempLevelData, m_SavePath);
        AssetDatabase.SaveAssets();

    }

    #region SetDataOnSceneFunctions
    private void SetStoneData()
    {
        TempLevelData.SingleStonePositions = new List<Vector2>();
        m_SingleStonesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.SINGLE_STONE).ToList();
        m_SingleStonesOnScene.ForEach(_singleStone =>
        {
            TempLevelData.SingleStonePositions.Add(new Vector2(_singleStone.transform.position.x, _singleStone.transform.position.y));
        }
        );

        TempLevelData.HorizontalStonePositions = new List<Vector2>();
        m_HorizontalStonesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.HORIZONTAL_STONE).ToList();
        m_HorizontalStonesOnScene.ForEach(_horizontalStone =>
        {
            TempLevelData.HorizontalStonePositions.Add(new Vector2(_horizontalStone.transform.position.x, _horizontalStone.transform.position.y));
        }
        );

        TempLevelData.VerticalStonePositions = new List<Vector2>();
        m_VerticalStonesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.VERTICAL_STONE).ToList();
        m_VerticalStonesOnScene.ForEach(_verticalStone =>
        {
            TempLevelData.VerticalStonePositions.Add(new Vector2(_verticalStone.transform.position.x, _verticalStone.transform.position.y));
        }
        );
    }

    private void SetGroundData()
    {
        TempLevelData.GroundPositions = new List<Vector2>();
        m_GroundsOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.GROUND).ToList();
        m_GroundsOnScene.ForEach(_ground =>
        {
            TempLevelData.GroundPositions.Add(new Vector2(_ground.transform.position.x, _ground.transform.position.y));
        }
        );
    }

    private void SetTreeData()
    {
        TempLevelData.RedTreePositions = new List<Vector2>();
        m_RedTreesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.RED_TREE).ToList();
        m_RedTreesOnScene.ForEach(_redTree =>
        {
            TempLevelData.RedTreePositions.Add(new Vector2(_redTree.transform.position.x, _redTree.transform.position.y));
        }
        );

        TempLevelData.YellowTreePositions = new List<Vector2>();
        m_YellowTreesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.YELLOW_TREE).ToList();
        m_YellowTreesOnScene.ForEach(_yellowTree =>
        {
            TempLevelData.YellowTreePositions.Add(new Vector2(_yellowTree.transform.position.x, _yellowTree.transform.position.y));
        }
        );

        TempLevelData.OrangeTreePositions = new List<Vector2>();
        m_OrangeTreesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.ORANGE_TREE).ToList();
        m_OrangeTreesOnScene.ForEach(_orangeTree =>
        {
            TempLevelData.OrangeTreePositions.Add(new Vector2(_orangeTree.transform.position.x, _orangeTree.transform.position.y));
        }
        );

        TempLevelData.GreenTreePositions = new List<Vector2>();
        m_GreenTreesOnScene = GameObject.FindGameObjectsWithTag(ObjectTags.GREEN_TREE).ToList();
        m_GreenTreesOnScene.ForEach(_greenTree =>
        {
            TempLevelData.GreenTreePositions.Add(new Vector2(_greenTree.transform.position.x, _greenTree.transform.position.y));
        }
        );
    }

    private void SetTreasureGeneratorData()
    {
        TempLevelData.TreasureGeneratorPositions = new List<Vector2>();
        TempLevelData.TreasureGeneratorRefreshRates = new List<float>();
        TempLevelData.TreasureGeneratorDelays = new List<float>();
        TempLevelData.TreasureGeneratorRefreshPositions= new List<Vector2>();
        m_TreasureGeneratorsOnScene = FindObjectsOfType<TreasureGenerator>().ToList();
        m_TreasureGeneratorsOnScene.ForEach(_treasureGenerator =>
        {
            TempLevelData.TreasureGeneratorPositions.Add(new Vector2(_treasureGenerator.transform.position.x, _treasureGenerator.transform.position.y));
            TempLevelData.TreasureGeneratorRefreshRates.Add(_treasureGenerator.GenerateRefreshRate);
            TempLevelData.TreasureGeneratorDelays.Add(_treasureGenerator.GenerateDelay);
            TempLevelData.TreasureGeneratorRefreshPositions.Add(new Vector2(_treasureGenerator.RefreshPosition.position.x, _treasureGenerator.RefreshPosition.position.y));
        }
        );
    }
    #endregion

    public void LoadData()
    {

    }
}
#endif

