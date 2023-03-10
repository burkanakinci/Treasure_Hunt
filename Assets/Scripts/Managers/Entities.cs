using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class Entities : CustomBehaviour
{
    [System.Serializable]
    public class StoneValue
    {
        public Sprite StoneSprite;
        public Vector2 StoneColliderOffset;
        public Vector2 StoneColliderSize;
    }
    #region Fields
    [SerializeField] private Transform[] m_ActiveParents;
    [SerializeField] private Sprite[] m_TreeSprites;
    [SerializeField] private StoneValue[] m_StoneValues;
    [SerializeField] private Vector2[] m_Directions;

    public int RemainingMiner => m_Miners.Count;
    #endregion

    #region SceneObjects
    private List<GameObject> m_GroundOnScene;
    private List<TreasureGenerator> m_TreasureGeneratorOnScene;
    private List<BaseMiner> m_Miners;
    #endregion

    #region Events 
    public event Action OnFreezeAllMiner;
    public event Action<List<BaseMiner>> OnOrderMiner;
    #endregion

    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;

        m_GroundOnScene = new List<GameObject>();
        m_TreasureGeneratorOnScene = new List<TreasureGenerator>();
        m_Miners = new List<BaseMiner>();
    }

    #region ManageList
    public void ManageTreasureGeneratorList(TreasureGenerator _treasureGenerator, ListOperation _operation)
    {
        if (_operation == ListOperation.Adding)
        {
            if (!m_TreasureGeneratorOnScene.Contains(_treasureGenerator))
            {
                m_TreasureGeneratorOnScene.Add(_treasureGenerator);
            }
        }
        else if (_operation == ListOperation.Subtraction)
        {
            if (m_TreasureGeneratorOnScene.Contains(_treasureGenerator))
            {
                m_TreasureGeneratorOnScene.Remove(_treasureGenerator);
            }
        }
    }
    public void ManageGroundList(GameObject _ground, ListOperation _operation)
    {
        if (_operation == ListOperation.Adding)
        {
            if (!m_GroundOnScene.Contains(_ground))
            {
                m_GroundOnScene.Add(_ground);
            }
        }
        else if (_operation == ListOperation.Subtraction)
        {
            if (m_GroundOnScene.Contains(_ground))
            {
                m_GroundOnScene.Remove(_ground);
            }
        }
    }
    public void ManagerMinerList(BaseMiner _miner, ListOperation _operation)
    {
        if (_operation == ListOperation.Adding)
        {
            if (!m_Miners.Contains(_miner))
            {
                m_Miners.Add(_miner);
            }
        }
        else if (_operation == ListOperation.Subtraction)
        {
            if (m_Miners.Contains(_miner))
            {
                m_Miners.Remove(_miner);
            }
        }
    }
    #endregion

    public void OrderMinerCollectedTreasure()
    {
        m_Miners = m_Miners.OrderByDescending((_collectedTreasure) => (_collectedTreasure.MinerCollectedTreasure)).ToList();
        OnOrderMiner?.Invoke(m_Miners);
    }

    public BaseMiner GetLastMiner()
    {
        OrderMinerCollectedTreasure();
        return m_Miners[m_Miners.Count - 1];
    }

    public Vector2 GetDirection(int _direction)
    {
        return m_Directions[_direction];
    }

    public TreasureGenerator GetNearestGenerator(Vector2 _point)
    {
        return m_TreasureGeneratorOnScene.OrderBy((_treasure) => Vector2.Distance(_treasure.transform.position, _point)).ToList()[0];
    }

    public Vector2 GetRandomPointOnGrounds()
    {
        return ((new Vector3((UnityEngine.Random.Range(-10.0f, 10.0f)), (UnityEngine.Random.Range(-20.0f, 20.0f)), 0.0f)) +
            m_GroundOnScene[UnityEngine.Random.Range(0, m_GroundOnScene.Count - 1)].transform.position);
    }

    private Vector2 m_TempSpawnTreasurePosition, m_NearestTreasurePosition;
    private float m_TempSpawnedMinDistance;
    public Vector2 SpawnNewTreasureGenerator()
    {
        m_TempSpawnTreasurePosition = GetRandomPointOnGrounds();

        m_NearestTreasurePosition = GetNearestGenerator(m_TempSpawnTreasurePosition).transform.position;
        m_TempSpawnedMinDistance = Vector2.Distance(m_NearestTreasurePosition, m_TempSpawnTreasurePosition);

        if (m_TempSpawnedMinDistance <= 25.0f)
        {
            return SpawnNewTreasureGenerator();
        }
        else
        {
            return m_TempSpawnTreasurePosition;
        }
    }

    public Transform GetActiveParent(ActiveParents _activeParent)
    {
        return m_ActiveParents[(int)_activeParent];
    }
    public Sprite GetTreeSprite(TreeType _treeType)
    {
        return m_TreeSprites[(int)_treeType];
    }

    public StoneValue GetStoneValue(StoneType _stoneType)
    {
        return m_StoneValues[(int)_stoneType];
    }

    #region Events 
    public void FreezeAllMiner()
    {
        OnFreezeAllMiner?.Invoke();
    }
    private void OnResetToMainMenu()
    {
        m_GroundOnScene.Clear();
        m_TreasureGeneratorOnScene.Clear();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion

}