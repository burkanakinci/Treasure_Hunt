
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : CustomBehaviour
{
    #region Fields
    private LevelData m_LevelData;
    private int m_CurrentLevelNumber;
    private int m_ActiveLevelDataNumber;
    private int m_MaxLevelDataCount;
    #endregion

    #region ExternalAccess
    public LevelData LevelData => m_LevelData;
    #endregion

    #region Actions
    public event Action OnCleanSceneObject;
    #endregion


    public override void Initialize()
    {

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;

        m_MaxLevelDataCount = Resources.LoadAll("LevelData", typeof(LevelData)).Length;
    }

    private void GetLevelData()
    {
        m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount) ? (m_CurrentLevelNumber) : ((int)(UnityEngine.Random.Range(1, (m_MaxLevelDataCount + 1))));
        m_LevelData = Resources.Load<LevelData>("LevelDatas/" + m_ActiveLevelDataNumber + "LevelData");
    }

    private void SpawnSceneObjects()
    {
        SpawnTreasures();
        SpawnTrees();
        SpawnStones();
        SpawnGrounds();
    }
    #region SpawnSceneObject

    private TreasureGenerator m_TempTreasureGenerator;
    private void SpawnTreasures()
    {
        for (int _treasureCount = m_LevelData.TreasureGeneratorPositions.Count - 1; _treasureCount >= 0; _treasureCount--)
        {
            m_TempTreasureGenerator = GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.TREASURE_GENERATOR),
                (m_LevelData.TreasureGeneratorPositions[_treasureCount]),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.TreasureGenerator))
            ).GetGameObject().GetComponent<TreasureGenerator>();

            m_TempTreasureGenerator.GenerateRefreshRate = m_LevelData.TreasureGeneratorRefreshRates[_treasureCount];
            m_TempTreasureGenerator.GenerateDelay = m_LevelData.TreasureGeneratorDelays[_treasureCount];
            m_TempTreasureGenerator.RefreshPosition.position = m_LevelData.TreasureGeneratorRefreshPositions[_treasureCount];
        }
    }

    private TreeObstacle m_TempSpawnedTree;
    private void SpawnTrees()
    {
        for (int _treeCount = m_LevelData.TreePositions.Count - 1; _treeCount >= 0; _treeCount--)
        {
            m_TempSpawnedTree = GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.TREE),
                (m_LevelData.TreePositions[_treeCount]),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Tree))
            ).GetGameObject().GetComponent<TreeObstacle>();

            m_TempSpawnedTree.SetTree(m_LevelData.TreeTypes[_treeCount]);
        }
    }

    private Stone m_TempSpawnedStone;
    private void SpawnStones()
    {
        for (int _stoneCount = m_LevelData.StonePositions.Count - 1; _stoneCount >= 0; _stoneCount--)
        {
            m_TempSpawnedStone = GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.STONE),
                (m_LevelData.StonePositions[_stoneCount]),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Stone))
            ).GetGameObject().GetComponent<Stone>();

            m_TempSpawnedStone.SetStone(m_LevelData.StoneTypes[_stoneCount]);
        }
    }


    private void SpawnGrounds()
    {
        for (int _groundCount = m_LevelData.GroundPositions.Count - 1; _groundCount >= 0; _groundCount--)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.GROUND),
                (m_LevelData.GroundPositions[_groundCount]),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Ground))
            );

            
        }
    }

    #endregion

    #region Events
    private void OnResetToMainMenu()
    {
        OnCleanSceneObject?.Invoke();

        m_CurrentLevelNumber = GameManager.Instance.PlayerManager.GetLevelNumber();

        GetLevelData();
        SpawnSceneObjects();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion
}