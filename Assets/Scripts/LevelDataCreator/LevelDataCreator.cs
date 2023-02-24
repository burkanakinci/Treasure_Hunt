
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
    private List<TreeObstacle> m_TreesOnScene;
    private List<Stone> m_StonesOnScene;
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
        TempLevelData.StonePositions = new List<Vector2>();
        TempLevelData.StoneTypes = new List<StoneType>();
        m_StonesOnScene = FindObjectsOfType<Stone>().ToList();
        m_StonesOnScene.ForEach(_stone =>
        {
            TempLevelData.StonePositions.Add(new Vector2(_stone.transform.position.x, _stone.transform.position.y));
            TempLevelData.StoneTypes.Add(_stone.StoneCurrentType);
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
        TempLevelData.TreePositions = new List<Vector2>();
        TempLevelData.TreeTypes = new List<TreeType>();
        m_TreesOnScene = FindObjectsOfType<TreeObstacle>().ToList();
        m_TreesOnScene.ForEach(_tree =>
        {
            TempLevelData.TreePositions.Add(new Vector2(_tree.transform.position.x, _tree.transform.position.y));
            TempLevelData.TreeTypes.Add(_tree.TreeCurrentType);
        }
        );
    }

    private void SetTreasureGeneratorData()
    {
        TempLevelData.TreasureGeneratorPositions = new List<Vector2>();
        TempLevelData.TreasureGeneratorRefreshRates = new List<float>();
        m_TreasureGeneratorsOnScene = FindObjectsOfType<TreasureGenerator>().ToList();
        m_TreasureGeneratorsOnScene.ForEach(_treasureGenerator =>
        {
            TempLevelData.TreasureGeneratorPositions.Add(new Vector2(_treasureGenerator.transform.position.x, _treasureGenerator.transform.position.y));
            TempLevelData.TreasureGeneratorRefreshRates.Add(_treasureGenerator.GenerateRefreshRate);
        }
        );
    }
    #endregion

    public void LoadData()
    {

    }
}
#endif

