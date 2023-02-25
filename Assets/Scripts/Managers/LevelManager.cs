
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        SpawnOpponent();
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
            GameManager.Instance.Entities.ManageGroundList((
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.GROUND),
                    (m_LevelData.GroundPositions[_groundCount]),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Ground))
                ).GetGameObject().gameObject),
                (ListOperation.Adding)
            );

            List<Vector2> m_TempSameYPositions = new List<Vector2>();
            List<Vector2> m_TempSameXPositions = new List<Vector2>();

            m_TempSameYPositions.AddRange(m_LevelData.GroundPositions.FindAll((_ground) => _ground.y == m_LevelData.GroundPositions[_groundCount].y));
            m_TempSameYPositions = m_TempSameYPositions.OrderBy(_sameY => _sameY.x).ToList();

            m_TempSameXPositions.AddRange(m_LevelData.GroundPositions.FindAll((_ground) => _ground.x == m_LevelData.GroundPositions[_groundCount].x));
            m_TempSameXPositions = m_TempSameXPositions.OrderBy(_sameX => _sameX.y).ToList();

            if ((m_LevelData.GroundPositions[_groundCount]) == m_TempSameYPositions[0])
            {
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.GROUND),
                    (m_LevelData.GroundPositions[_groundCount] - new Vector2(32.40967f, 0.0f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Ground))
                );
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.SIDE_LOG),
                    (m_LevelData.GroundPositions[_groundCount] - new Vector2(15.45483f, 0.0f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Log))
                );
            }
            if ((m_LevelData.GroundPositions[_groundCount]) == m_TempSameYPositions[m_TempSameYPositions.Count - 1])
            {
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.GROUND),
                    (m_LevelData.GroundPositions[_groundCount] + new Vector2(32.40967f, 0.0f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Ground))
                );
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.SIDE_LOG),
                    (m_LevelData.GroundPositions[_groundCount] + new Vector2(15.45483f, 0.0f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Log))
                );
            }

            if ((m_LevelData.GroundPositions[_groundCount]) == m_TempSameXPositions[0])
            {
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.GROUND),
                    (m_LevelData.GroundPositions[_groundCount] - new Vector2(0.0f, 45.0f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Ground))
                );
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.UP_LOG),
                    (m_LevelData.GroundPositions[_groundCount] - new Vector2(0.0f, 22.31f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Log))
                );
            }
            if ((m_LevelData.GroundPositions[_groundCount]) == m_TempSameXPositions[m_TempSameXPositions.Count - 1])
            {
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.GROUND),
                    (m_LevelData.GroundPositions[_groundCount] + new Vector2(0.0f, 45.0f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Ground))
                );
                GameManager.Instance.ObjectPool.SpawnFromPool(
                    (PooledObjectTags.UP_LOG),
                    (m_LevelData.GroundPositions[_groundCount] + new Vector2(0.0f, 22.31f)),
                    (Quaternion.identity),
                    (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Log))
                );
            }
        }
    }

    private OpponentMiner m_TempSpawnedOpponent;
    private void SpawnOpponent()
    {
        for (int _opponentCount = m_LevelData.Opponents.Count - 1; _opponentCount >= 0; _opponentCount--)
        {
            m_TempSpawnedOpponent = GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.OPPONENT),
                (Vector2.zero),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.Opponent))
            ).GetGameObject().GetComponent<OpponentMiner>();

            m_TempSpawnedOpponent.OpponentCurrentDifficulty = m_LevelData.Opponents[_opponentCount];
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
